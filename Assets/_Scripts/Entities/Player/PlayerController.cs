using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[RequireComponent(typeof(PlayerMover))]

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : EntityController<PlayerMover,PlayerCharacterClassVisual,PlayerStats,PlayerBaseStats,PlayerBaseMoveStats>
{
    [HideInInspector] public PlayerCharacterClass characterClass{get; private set;}  
    [HideInInspector] public GameInput gameInput{get; private set;}
    [HideInInspector] public PlayerStateMachine playerStateMachine{get; private set;}
    [HideInInspector] public PlayerAirControlStateMachine playerAirControlStateMachine{get; private set;}
    [HideInInspector] public PlayerHitBox playerHitBox;
    public bool jumpIsPressed{get; private set;}
    public bool jumpIsHelded{get; private set;}
    public float currentTimerJumpBuffer{get; private set;}
    public float currentTimerDashCoolDown{get; private set;}
    public float currentTimerBlockCoolDown{get; private set;}
    public float currentInvencibilityTime{get; private set;}
    public string skillSlot1;
    public string skillSlot2;
    public float currentSkill1Cooldown;
    public float currentSkill2Cooldown;
    public int currentSkillPressed;
    public Vector3 checkPoint;
    protected override void Awake()
    {
        base.Awake();
        characterClass=GetComponentInChildren<PlayerCharacterClass>();
        playerHitBox=GetComponentInChildren<PlayerHitBox>();
        GameManager.instance.player=this;       // TIRE ISSO DEPOISSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSs
    }

    protected override void Start()
    {
        base.Start();
        isFacingRight=true;
        currentTimerJumpBuffer=0;
        currentTimerDashCoolDown=0;
        currentTimerBlockCoolDown=0;
        currentInvencibilityTime=0;
        currentSkill1Cooldown=0;
        currentSkill2Cooldown=0;
        SetupGameInput();
        SetupStateMachine();
        playerHitBox.OnHit+=Hitted;
        //HUDManager.instance.OnSkillSelected+=GetNewSkill;
        stats.OnDeath+=Dead;

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
        playerHitBox.HandleHitBox(playerStateMachine.currentState.invencible || currentInvencibilityTime>0);
        playerStateMachine.Action(Time.deltaTime);
        playerAirControlStateMachine.Action(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        HandleExternalForces();
        IsGrounded=mover.CheckGround();
        IsHeadBump=mover.CheckforHeadBump();
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
        currentInvencibilityTime=Math.Max(0,currentInvencibilityTime-Time.deltaTime);
        currentSkill1Cooldown=Math.Max(0,currentSkill1Cooldown-Time.deltaTime);
        currentSkill2Cooldown=Math.Max(0,currentSkill2Cooldown-Time.deltaTime);
        if (skillSlot1 != "")
        {
            HUDManager.instance.skillSlot1Cooldown.fillAmount=currentSkill1Cooldown/characterClass.skillS[skillSlot1].cooldown;
        }
        if (skillSlot2 != "")
        {
            HUDManager.instance.skillSlot2Cooldown.fillAmount=currentSkill2Cooldown/characterClass.skillS[skillSlot2].cooldown;
        }
        
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
    public void Skill1Pressed(object sender, EventArgs e)
    {
        currentSkillPressed=1;
        if (skillSlot1 == "")
        {
            return;
        }
        if (playerStateMachine.GetState(PlayerStateName.SkillCast).CheckTrasitionConditions() && currentSkill1Cooldown<=0)
        {
            playerStateMachine.SwitchState(PlayerStateName.SkillCast);
        }
    }
    public void Skill2Pressed(object sender, EventArgs e)
    {
        currentSkillPressed=2;
        if (skillSlot2 == "")
        {
            return;
        }
        if (playerStateMachine.GetState(PlayerStateName.SkillCast).CheckTrasitionConditions() && currentSkill2Cooldown<=0)
        {
            playerStateMachine.SwitchState(PlayerStateName.SkillCast);
        }
    }

    public void GetNewSkill(object sender, HUDManager.SkillInfoSelected skillInfoSelected)
    {
        int slot;
        if (skillInfoSelected.slot == 0)
        {
            if (skillSlot1 == "")
            {
                slot=1;
            }
            else if(skillSlot2=="")
            {
                slot=2;
            }
            else
            {
                slot=1;
            }
        }
        else
        {
            slot=skillInfoSelected.slot;
        }
        if (slot == 1)
        {
            HUDManager.instance.skillSlot1.sprite=skillInfoSelected.skillinfo.UIinfo.skillSprite;
            HUDManager.instance.skillSlot1Cooldown.sprite=skillInfoSelected.skillinfo.UIinfo.skillSprite;
            skillSlot1=skillInfoSelected.skillinfo.skillName;
        }
        else
        {
            HUDManager.instance.skillSlot2.sprite=skillInfoSelected.skillinfo.UIinfo.skillSprite;
            HUDManager.instance.skillSlot2Cooldown.sprite=skillInfoSelected.skillinfo.UIinfo.skillSprite;
            skillSlot2=skillInfoSelected.skillinfo.skillName;
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
        gameInput.OnSkill1Pressed+=Skill1Pressed;
        gameInput.OnSkill2Pressed+=Skill2Pressed;
        gameInput.OnDashPressed+=DashPressed;
        gameInput.OnBlockPressed+=BlockPressed;
        moveDir=gameInput.GetNormalizedMovementInput(); 
    }

    private void DisabelGameInput()
    {
        gameInput.OnJumpPressed-=JumpPressed;
        gameInput.OnJumpHelded-=JumpHelded;
        gameInput.OnAttackPressed-=AttackPressed;
        gameInput.OnSkill1Pressed-=Skill1Pressed;
        gameInput.OnSkill2Pressed-=Skill2Pressed;
        gameInput.OnBlockPressed-=BlockPressed;
        gameInput.OnDashPressed-=DashPressed;
    }

    public void Hitted(object sender,HitInfo hitInfo)
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
        currentInvencibilityTime=stats.baseStats.invencibilityTime;
        stats.TakeDamage(hitInfo.damage);
        playerStateMachine.SwitchState(PlayerStateName.Hurt);
        visual.StartFlahshing(stats.baseStats.flashingInterval,stats.baseStats.invencibilityTime);
    }

    public void HittedByTrap()
    {
        ReturnToCheckPoint();
        currentInvencibilityTime=stats.baseStats.invencibilityTime;
        stats.TakeTrapDamage();
        visual.StartFlahshing(stats.baseStats.flashingInterval,stats.baseStats.invencibilityTime);
    }
    public void ReturnToCheckPoint()
    {
        playerStateMachine.SwitchState(PlayerStateName.Idle);
        transform.position=checkPoint;
    }
    public void NewLevel(Vector3 levelStartPos)
    {
        playerStateMachine.SwitchState(PlayerStateName.Idle);
        checkPoint=levelStartPos;
        transform.position=levelStartPos;
    }
    public void Dead(object sender, EventArgs e)
    {
        playerStateMachine.SwitchState(PlayerStateName.Death);
        DestroyPlayerHitBoxes();
        GameManager.instance.GameOver();
        enabled=false;
    }

    private void DestroyPlayerHitBoxes()
    {
        playerHitBox.DestroyPlayerHitBox();
    }
}
