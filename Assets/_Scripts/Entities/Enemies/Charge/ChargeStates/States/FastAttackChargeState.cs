using System;
using UnityEngine;

public class FastAttackChargeState : ChargeState
{
    float actualTime;
    int numberOfAttackInList=1;
    public FastAttackChargeState(ChargeStateMachine parent, ChargeController charge) : base(parent, ChargeStateName.FastAttack, charge)
    {
    }
    public override void EntryState()
    {
        base.EntryState();
        actualTime=enemy.stats.baseStats.attackDatas[numberOfAttackInList].attackTime;
        charge.visual.OnInitiateOfFastAttackAnimation+=InitiateOfFastAttackAnimation;
        HandleMoveDir();
        HandleRotation();
        enemy.SetHorizontalFrameVelocity(0f);
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(ChargeStateName.Cooling);
            return;
        }
        if (!IsBounded())
        {
            parent.SwitchState(ChargeStateName.GoBack);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
    }

    public override void ExitState()
    {
        base.ExitState();
        charge.visual.OnInitiateOfFastAttackAnimation-=InitiateOfFastAttackAnimation;
        enemy.attacksCooldowns[numberOfAttackInList]=enemy.stats.baseStats.attackDatas[numberOfAttackInList].cooldown;
        enemy.chargeWeapon.DisableWeapon(numberOfAttackInList);
    }
    protected void InitiateOfFastAttackAnimation(object sender, EventArgs e)
    {
        enemy.chargeWeapon.EnableWeapon(numberOfAttackInList);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.Normalize(enemy.GetPlayerPos()-enemy.transform.position);
    }
}
