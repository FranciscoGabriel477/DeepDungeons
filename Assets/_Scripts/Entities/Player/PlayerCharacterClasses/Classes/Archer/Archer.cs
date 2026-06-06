using System;
using UnityEngine;

public class ArcherClass : PlayerCharacterClass
{
    public ArcherWeaponInfo weaponInfo;
    public ArcherVisual archerVisual;
    public ContactFilter2D contactFilter;

    private void Awake()
    {
        
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
        attackStateMachine.RegisterState(new RangeAttackState1(attackStateMachine,playerController));
    }
    public override void HandleAttack()
    {
        
        if (attackStateMachine.GetActualStateName() == "NotAttack")
        {
            attackStateMachine.SwitchState(ArcherAttackName.RangeAttack1);
        }
    }

    public void SummomArrow(float dir)
    {
        GameObject arrowGameObject=Instantiate(weaponInfo.ammoType,transform.position,Quaternion.identity);
        arrowGameObject.transform.Rotate(0,dir,0);
        PlayerArrow arrow=arrowGameObject.GetComponent<PlayerArrow>();
        arrow.direction=dir==0?Vector2.right:Vector2.left;
    }

    public override float GetStaminaAttackCost()
    {
        return weaponInfo.staminaCostOnRange1;
    }
}
