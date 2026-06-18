using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierClass : PlayerCharacterClass
{
    public SoldierWeaponInfo weaponInfo;
    public SoldierVisual soldierVisual;
    public ChargeCutInfo chargeCutInfo;
    public ThrowAxeInfo throwAxeInfo;

    private void Awake()
    {
        SetupSkillsDictionary();
    }

    private void Start()
    {
        SetupAttacckStateMachine();
        SetupSkillStateMachine();
        playerController.playerHitBox.OnAxeTaked+=AxeRecovery;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    private void SetupAttacckStateMachine()
    {
        attackStateMachine=new ClassAttackStateMachine(playerController);
        attackStateMachine.RegisterState(new MeeleAttackState1(attackStateMachine,playerController));
        attackStateMachine.RegisterState(new AirMeeleAttackState(attackStateMachine,playerController));
        attackStateMachine.OnStateChanged+=soldierVisual.AttackStateChanged;
    }

    private void SetupSkillStateMachine()
    {
        skillStateMachine= new ClassSkillStateMachine(playerController);
        skillStateMachine.RegisterState(new ChargeCutSkillState(skillStateMachine,playerController));
        skillStateMachine.RegisterState(new ThrowAxeSkillState(skillStateMachine,playerController));
        skillStateMachine.OnStateChanged+=soldierVisual.SkillStateChanged;
    }
    public override void HandleAttack()
    {
        
        if (attackStateMachine.GetActualStateName() == PlayerStateName.NotAttacking)
        {
            if (playerController.IsGrounded)
            {
                attackStateMachine.SwitchState(SoldierAttackName.MeeleAttack1);
            }
            else
            {
                attackStateMachine.SwitchState(SoldierAttackName.AirMeeleAttack);
            }
        }
    }
    public override float GetStaminaAttackCost()
    {
        return weaponInfo.staminaCostOnMeele1;
    }

    public override void SetupSkillsDictionary()
    {
        skillS= new Dictionary<string, SkillInfo>
        {
            { chargeCutInfo.skillName, chargeCutInfo },
            { throwAxeInfo.skillName, throwAxeInfo   }
        };
    }

    protected void AxeRecovery(object sender, PlayerHitBox.CooldownRecovery recovery)
    {
        if (playerController.skillSlot1 == SoldierSkillName.ThrowAxe){
            playerController.OnSkill1CooldownReduced?.Invoke(playerController, new PlayerController.CooldownCount{cooldown=recovery.colldownRecovery});
        }
        else if(playerController.skillSlot2 == SoldierSkillName.ThrowAxe)
        {
            playerController.OnSkill2CooldownReduced?.Invoke(playerController, new PlayerController.CooldownCount{cooldown=recovery.colldownRecovery});
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(weaponInfo.attackMeele1OffSet,weaponInfo.attackMeele1Size);
    }
}