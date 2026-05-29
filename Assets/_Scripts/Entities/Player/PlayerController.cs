using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : EntityController
{
    public PlayerMover playerMover{get; private set;} 
    public PlayerCharacterClassVisual playerVisual{get; private set;}  
    public PlayerCharacterClass characterClass{get; private set;}  
    public GameInput gameInput{get; private set;}
    public PlayerBaseMoveStats baseMoveStats;
    public PlayerBaseStats baseStats;
    public PlayerStateMachine playerStateMachine{get; private set;}
    public PlayerAirControlStateMachine playerAirControlStateMachine{get; private set;}
    private float currentTimerJumpBuffer=0;
    public bool jumpIsPressed{get; private set;}
    public bool jumpIsHelded{get; private set;}
    public void Awake()
    {
        
        playerMover=GetComponent<PlayerMover>();
        playerVisual=GetComponentInChildren<PlayerCharacterClassVisual>();
        characterClass=GetComponentInChildren<PlayerCharacterClass>();
        
    }

    private void Start()
    {
        SetupGameInput();
        IsGrounded=playerMover.CheckGround();
        isFacingRight=true;
        playerStateMachine= new PlayerStateMachine(this);
        playerAirControlStateMachine= new PlayerAirControlStateMachine(this);
        playerStateMachine.OnStateChanged += playerVisual.MainStateChanged;
        playerAirControlStateMachine.OnStateChanged += playerVisual.AirStateChanged;
    }

    public void Update()
    {
        CountTimers();
        HandleMoveDir();
        playerStateMachine.Action(Time.deltaTime);
        playerAirControlStateMachine.Action(Time.deltaTime);
        //Debug.Log(playerStateMachine.GetActualStateName());
    }

    public void FixedUpdate()
    {
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,baseMoveStats.knockBackDecelaration*Time.fixedDeltaTime);
        IsGrounded=playerMover.CheckGround();

        if (playerStateMachine.AllowsRotate())
        {
            HandleRotation();
        }
        playerStateMachine.FixedAction(Time.fixedDeltaTime);
        playerAirControlStateMachine.FixedAction(Time.fixedDeltaTime);
        Move();
    }

    public void OnDestroy()
    {
        DisabelGameInput();
    }
    private void CountTimers()
    {
        currentTimerJumpBuffer=Math.Max(0,currentTimerJumpBuffer-Time.deltaTime);
    }
    public void ResetJumpBuffer()
    {
        currentTimerJumpBuffer=0;
    }
    public bool CheckJumpConditions()
    {
        return currentTimerJumpBuffer>0f && IsGrounded && playerStateMachine.AllowsJump();
    }
    public void Move()
    {
        playerMover.SetVelocity(frameVelocity+externalForce);
    }

    public override void SetHorizontalFrameVelocity(float newVelocityDirX)
    {
        frameVelocity.x=newVelocityDirX;
    }

    public override void SetVerticalFrameVelocity(float newVelocityY)
    {
        frameVelocity.y=newVelocityY;
        frameVelocity.y=Math.Clamp(frameVelocity.y,-baseMoveStats.maxVerticalSpeed,baseMoveStats.maxVerticalSpeed);
    }
    protected virtual void HandleRotation()
    {
        if (isFacingRight && moveDir.x<0)
        {
            isFacingRight=false;
            transform.Rotate(0,-180f,0);
        }  
        else if(!isFacingRight && moveDir.x>0)
        {
            isFacingRight=true;
            transform.Rotate(0,180f,0);
        }
    }

    protected virtual void HandleMoveDir()
    {
        moveDir=gameInput.GetNormalizedMovementInput();
    }
     public void AttackPressed(object sender, EventArgs e)
    {
        if (playerStateMachine.GetState(PlayerStateName.Attack).CheckTrasitionConditions())
        {
            playerStateMachine.SwitchState(PlayerStateName.Attack);
        }
    }

    public void DashPressed(object sender, EventArgs e)
    {
        if (playerStateMachine.GetState(PlayerStateName.Dash).CheckTrasitionConditions())
        {
            playerStateMachine.SwitchState(PlayerStateName.Dash);
        }
    }
    public void JumpPressed(object sender, EventArgs e)
    {
        currentTimerJumpBuffer=baseMoveStats.jumpBufferTimer;   
        jumpIsHelded=false;
        jumpIsPressed=true;
    }
    public void JumpHelded(object sender, EventArgs e)
    {
        jumpIsHelded=true;
        jumpIsPressed=false;
    }
    public void BlockPressed(object sender, EventArgs e)
    {
        if (playerStateMachine.GetState(PlayerStateName.Block).CheckTrasitionConditions())
        {
            playerStateMachine.SwitchState(PlayerStateName.Block);
        }
    }
    private void SetupGameInput()
    {
        gameInput = GameInput.instance;
        gameInput.OnJumpPressed+=JumpPressed;
        gameInput.OnJumpHelded+=JumpHelded;
        gameInput.OnAttackPressed+=AttackPressed;
        gameInput.OnDashPressed+=DashPressed;
        gameInput.OnBlockPressed+=BlockPressed;
        moveDir=gameInput.GetNormalizedMovementInput(); 
    }

    private void DisabelGameInput()
    {
        gameInput.OnJumpPressed-=JumpPressed;
        gameInput.OnJumpHelded-=JumpHelded;
        gameInput.OnAttackPressed-=AttackPressed;
        gameInput.OnBlockPressed-=BlockPressed;
        gameInput.OnDashPressed-=DashPressed;
    }

    public override void GetHit(HitInfo hitInfo)
    {
        if (playerStateMachine.GetActualStateName() == PlayerStateName.Block)
        {
            BlockPlayerState blockPlayerState=playerStateMachine.GetState(PlayerStateName.Block) as BlockPlayerState;
            if (blockPlayerState.TryBlock(hitInfo))
            {
                Debug.Log("Defendeu");
                return;
            }
        }
        base.GetHit(hitInfo);
        playerStateMachine.SwitchState(PlayerStateName.Hurt);
    }

}
