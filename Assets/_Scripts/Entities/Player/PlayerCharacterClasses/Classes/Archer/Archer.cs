using System;
using System.Collections.Generic;
using UnityEngine;

public class ArcherClass : PlayerCharacterClass
{
    public ArcherWeaponInfo weaponInfo;
    public ArcherVisual archerVisual;
    public ContactFilter2D contactFilter;
    public Vector3 shotPoint;

    public ToxicArrowInfo toxicArrowInfo;

    private void Awake()
    {
        SetupSkillsDictionary();
    }

    private void Start()
    {
        SetupAttackStateMachine();
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
    private void SetupAttackStateMachine()
    {
        attackStateMachine=new ClassAttackStateMachine(playerController);
        attackStateMachine.RegisterState(new RangeAttackState1(attackStateMachine,playerController));
        attackStateMachine.RegisterState(new AirRangeAttackState(attackStateMachine,playerController));
        attackStateMachine.OnStateChanged+=archerVisual.AttackStateChanged;
    }
    private void SetupSkillStateMachine()
    {
        skillStateMachine= new ClassSkillStateMachine(playerController);
        skillStateMachine.RegisterState(new ToxicArrowSkillState(skillStateMachine,playerController));
        skillStateMachine.OnStateChanged+=archerVisual.SkillStateChanged;
    }
    public override void HandleAttack()
    {
        
        if (attackStateMachine.GetActualStateName() == PlayerStateName.NotAttacking)
        {
            if (playerController.IsGrounded)
            {
                attackStateMachine.SwitchState(ArcherAttackName.RangeAttack1);
            }
            else
            {
                attackStateMachine.SwitchState(ArcherAttackName.AirRangeAttack);
            }
        }
    }

    public void SummomArrow(float dir)
    {
        GameObject arrowGameObject=Instantiate(weaponInfo.ammoType,transform.position+shotPoint,Quaternion.identity);
        arrowGameObject.transform.Rotate(0,dir,0);
        PlayerArrow arrow=arrowGameObject.GetComponent<PlayerArrow>();
        arrow.direction=dir==0?Vector2.right:Vector2.left;
    }

    public override float GetStaminaAttackCost()
    {
        return weaponInfo.staminaCostOnRange1;
    }

    public override void SetupSkillsDictionary()
    {
        skillS= new Dictionary<string, SkillInfo>
        {
            { toxicArrowInfo.skillName, toxicArrowInfo }
        };
    }
}
