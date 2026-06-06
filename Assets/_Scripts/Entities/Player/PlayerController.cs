using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMover))]

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : EntityController<PlayerMover,PlayerCharacterClassVisual,PlayerStats,PlayerBaseStats,PlayerBaseMoveStats>
{
    public PlayerCharacterClass characterClass{get; private set;}  
    public GameInput gameInput{get; private set;}
    public PlayerStateMachine playerStateMachine{get; private set;}
    public PlayerAirControlStateMachine playerAirControlStateMachine{get; private set;}
    public Collider2D playerHitBox;
    public bool jumpIsPressed{get; private set;}
    public bool jumpIsHelded{get; private set;}
    public float currentTimerJumpBuffer{get; private set;}
    public float currentTimerDashCoolDown{get; private set;}
    public float currentTimerBlockCoolDown{get; private set;}
    protected override void Awake()
    {
        
        base.Awake();
        characterClass=GetComponentInChildren<PlayerCharacterClass>();
        
    }

    protected override void Start()
    {
        base.Start();
        isFacingRight=true;
        currentTimerJumpBuffer=0;
        currentTimerDashCoolDown=0;
        currentTimerBlockCoolDown=0;
        SetupGameInput();
        SetupStateMachine();
        
    }

    private void SetupStateMachine()
    {
        playerStateMachine= new PlayerStateMachine(this);
        playerAirControlStateMachine= new PlayerAirControlStateMachine(this);
        playerStateMachine.OnStateChanged += visual.MainStateChanged;
        playerAirControlStateMachine.OnStateChanged += visual.AirStateChanged;
    }
    public void Update()
    {
        CountTimers();
        GetInputMoveDir();
        HandleEffects();
        playerStateMachine.Action(Time.deltaTime);
        playerAirControlStateMachine.Action(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        HandleExternalForces();
        IsGrounded=mover.CheckGround();
        playerStateMachine.FixedAction(Time.fixedDeltaTime);
        playerAirControlStateMachine.FixedAction(Time.fixedDeltaTime);
        Move();
    }

    protected override void HandleExternalForces()
    {
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,stats.baseMoveStats.knockBackDecelaration*Time.fixedDeltaTime);
    }
    public void OnDestroy()
    {
        DisabelGameInput();
    }
    private void CountTimers()
    {
        currentTimerJumpBuffer=Math.Max(0,currentTimerJumpBuffer-Time.deltaTime);
        currentTimerDashCoolDown=Math.Max(0,currentTimerDashCoolDown-Time.deltaTime);
        currentTimerBlockCoolDown=Math.Max(0,currentTimerBlockCoolDown-Time.deltaTime);
    }
    public void ResetJumpBuffer()
    {
        currentTimerJumpBuffer=0;
    }
    public void ResetDashCooldown()
    {
        currentTimerDashCoolDown=stats.baseMoveStats.dashCooldown;
    }
    public void ResetBlockCooldown()
    {
        currentTimerBlockCoolDown=stats.baseStats.blockCooldown;
    }
    public bool CheckJumpConditions()
    {
        return currentTimerJumpBuffer>0f && IsGrounded && playerStateMachine.AllowsJump();
    }
    public void Move()
    {
        mover.SetVelocity(frameVelocity+externalForce);
    }

    public override void SetHorizontalFrameVelocity(float newVelocityDirX)
    {
        frameVelocity.x=newVelocityDirX;
    }

    public override void SetVerticalFrameVelocity(float newVelocityY)
    {
        frameVelocity.y=newVelocityY;
        frameVelocity.y=Math.Clamp(frameVelocity.y,-stats.baseMoveStats.maxVerticalSpeed,stats.baseMoveStats.maxVerticalSpeed);
    }

    public virtual void GetInputMoveDir()
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
        currentTimerJumpBuffer=stats.baseMoveStats.jumpBufferTimer;   
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
                externalForce+=hitInfo.knockBack/2;
                externalForce=Vector2.ClampMagnitude(externalForce,stats.baseMoveStats.maxKnockBack);
                return;
            }
        }
        base.GetHit(hitInfo);
        stats.TakeDamage(hitInfo.damage);
        playerStateMachine.SwitchState(PlayerStateName.Hurt);
    }

}
