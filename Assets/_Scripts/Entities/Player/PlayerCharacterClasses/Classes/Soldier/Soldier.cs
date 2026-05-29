using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SoldierClass : PlayerCharacterClass
{
    public SoldierWeaponInfo weaponInfo;
    public SoldierVisual soldierVisual;
    public Collider2D weaponCollider{get; protected set;}
    public ContactFilter2D contactFilter;

    private void Awake()
    {
        weaponCollider=GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetupAttacckStateMachine();
    }
    private void Update()
    {
        attackStateMachine.Action(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        attackStateMachine.FixedAction(Time.fixedDeltaTime);
    }
    private void SetupAttacckStateMachine()
    {
        attackStateMachine=new ClassAttackStateMachine(playerController);
        attackStateMachine.RegisterState(new MeeleAttackState1(attackStateMachine,playerController));
    }
    public override void HandleAttack()
    {
        
        if (attackStateMachine.GetActualStateName() == "NotAttack")
        {
            attackStateMachine.SwitchState(SoldierAttackName.MeeleAttack1);
        }
    }
}
