using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SoldierClass : PlayerCharacterClass
{
    public SoldierWeaponInfo weaponInfo;
    public ContactFilter2D contactFilter;
    public SoldierVisual soldierVisual;
    public Collider2D weaponCollider{get; protected set;}
    public ChargeCutInfo chargeCutInfo;
    public ThrowAxeInfo throwAxeInfo;

    private void Awake()
    {
        weaponCollider=GetComponent<Collider2D>();
        SetupSkillsDictionary();
    }

    private void Start()
    {
        SetupAttacckStateMachine();
        SetupSkillStateMachine();
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
}