using System;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[RequireComponent(typeof(PlayerMover))]

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerCooldowns))]
public class PlayerController : EntityController<PlayerMover,PlayerCharacterClassVisual,PlayerStats,PlayerBaseStats,PlayerBaseMoveStats>
{
    [HideInInspector] public PlayerCharacterClass characterClass{get; private set;}  
    [HideInInspector] public GameInput gameInput{get; private set;}
    [HideInInspector] public PlayerStateMachine playerStateMachine{get; private set;}
    [HideInInspector] public PlayerAirControlStateMachine playerAirControlStateMachine{get; private set;}
    [HideInInspector] public PlayerHitBox playerHitBox;
    public bool jumpIsPressed{get; private set;}
    public bool jumpIsHelded{get; private set;}
    
    public PlayerCooldowns timers;
    public EventHandler<CooldownCount> OnSkill1EnterCooldown;
    public EventHandler<CooldownCount> OnSkill2EnterCooldown;
    public EventHandler<CooldownCount> OnSkill1CooldownReduced;
    public EventHandler<CooldownCount> OnSkill2CooldownReduced;
    public EventHandler<CooldownCount> OnDashEnterCooldown;
    public EventHandler<CooldownCount> OnBlockEnterCooldown;
    public EventHandler<CooldownCount> OnInvencibilityTimeStart;
    public EventHandler<CooldownCount> OnJumpBufferStart;
    public EventHandler<CooldownCount> OnCoyoteTimeStart;
    public class CooldownCount : EventArgs
    {
        public float cooldown;
    }
    public string skillSlot1;
    public string skillSlot2;
    public int currentSkillPressed;
    public Vector3 checkPoint;
    protected override void Awake()
    {
        base.Awake();
        characterClass=GetComponentInChildren<PlayerCharacterClass>();
        playerHitBox=GetComponentInChildren<PlayerHitBox>();
        timers=GetComponent<PlayerCooldowns>();
        GameManager.instance.player=this;       // TIRE ISSO DEPOISSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSs
    }

    protected override void Start()
    {
        base.Start();
        isFacingRight=true;
        SetupGameInput();
        SetupStateMachine();
        SetupTimers();
        playerHitBox.OnHit+=Hitted;
        //HUDManager.instance.OnSkillSelected+=GetNewSkill;
        stats.OnDeath+=Dead;

    }
    private void SetupTimers()
    {
        OnInvencibilityTimeStart+=timers.InvencibilityTimeStart;
        OnBlockEnterCooldown+=timers.BlockCooldownStart;
        OnDashEnterCooldown+=timers.DashCooldownStart;
        OnSkill1EnterCooldown+=timers.Skill1CooldownStart;
        OnSkill2EnterCooldown+=timers.Skill2CooldownStart;
        OnJumpBufferStart+=timers.JumpBufferStart;
        OnCoyoteTimeStart+=timers.CoyoteTimeStart;
        OnSkill1CooldownReduced+=timers.Skill1CooldownReduce;
        OnSkill2CooldownReduced+=timers.Skill2CooldownReduce;
    }
    public void Update()
    {
        playerStateMachine.Action(Time.deltaTime);
        playerAirControlStateMachine.Action(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        playerStateMachine.FixedAction(Time.fixedDeltaTime);
        playerAirControlStateMachine.FixedAction(Time.fixedDeltaTime);

    }

    private void SetupStateMachine()
    {
        playerStateMachine= new PlayerStateMachine(this);
        playerAirControlStateMachine= new PlayerAirControlStateMachine(this);
        playerStateMachine.OnStateChanged += visual.MainStateChanged;
        playerAirControlStateMachine.OnStateChanged += visual.AirStateChanged;
    }

    public void CheckGround()
    {
        bool pastGroundState=IsGrounded;
        IsGrounded=mover.CheckGround();
        if(!IsGrounded && pastGroundState)
        {
            OnCoyoteTimeStart?.Invoke(this, new CooldownCount{cooldown=stats.baseMoveStats.coyoteTime});
        }
        else if(IsGrounded && !pastGroundState)
        {
            ResetCoyoteTime();
        }
        IsHeadBump=mover.CheckforHeadBump();
    }

    public override void HandleExternalForces()
    {
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,stats.baseMoveStats.knockBackDecelaration*Time.fixedDeltaTime);
    }
    public void OnDestroy()
    {
        DisabelGameInput();
    }
    public void ResetJumpBuffer()
    {
        OnJumpBufferStart?.Invoke(this,new CooldownCount{cooldown=0});
    }
    public void ResetCoyoteTime()
    {
        OnCoyoteTimeStart?.Invoke(this, new CooldownCount{cooldown=0});
    }
    public void ResetBlockCooldown()
    {
        OnBlockEnterCooldown?.Invoke(this, new CooldownCount{cooldown=stats.baseStats.blockCooldown});
    }
    public void ResetDashCooldown()
    {
        OnDashEnterCooldown?.Invoke(this, new CooldownCount{cooldown=stats.baseMoveStats.dashCooldown});
    }
    public bool CheckJumpConditions()
    {
        return timers.InJumpBuffer() && (IsGrounded || timers.InCoyoteTime()) && playerStateMachine.AllowsJump();
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
        if (playerStateMachine.GetState(PlayerStateName.SkillCast).CheckTrasitionConditions() && !timers.Skill1InCooldown())
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
        if (playerStateMachine.GetState(PlayerStateName.SkillCast).CheckTrasitionConditions() && !timers.Skill2InCooldown())
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
        OnJumpBufferStart?.Invoke(this,new CooldownCount{cooldown=stats.baseMoveStats.jumpBufferTimer});   
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
        gameInput.OnBlockPressed+=BlockPressed;
        gameInput.OnDashPressed+=DashPressed;
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
        if (playerStateMachine.GetActualStateName() == PlayerStateName.Death)
        {
            return;
        }
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
        OnInvencibilityTimeStart?.Invoke(this, new CooldownCount{cooldown=stats.baseStats.invencibilityTime});
        stats.TakeDamage(hitInfo.damage);
        playerStateMachine.SwitchState(PlayerStateName.Hurt);
        visual.StartFlahshing(stats.baseStats.flashingInterval,stats.baseStats.invencibilityTime);
    }

    public void HittedByTrap()
    {
        if (playerStateMachine.GetActualStateName() == PlayerStateName.Death)
        {
            return;
        }
        ReturnToCheckPoint();
        OnInvencibilityTimeStart?.Invoke(this, new CooldownCount{cooldown=stats.baseStats.invencibilityTime});
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
        visual.enabled=false;
        mover.enabled=false;
        stats.enabled=false;
        characterClass.enabled=false;
        enabled=false;
        GameManager.instance.GameOver();
    }

    private void DestroyPlayerHitBoxes()
    {
        playerHitBox.DestroyPlayerHitBox();
    }
}
