using System;
using UnityEngine;

public class AttackChargeState : ChargeState
{
    float actualTime;
    public AttackChargeState(ChargeStateMachine parent, ChargeController charge) : base(parent, ChargeStateName.Attack, charge){}
    public override void EntryState()
    {
        base.EntryState();
        actualTime=enemy.stats.baseStats.attackTime;
        charge.visual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
        HandleMoveDir();
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
       HandleHorizontalMomentum();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.chargeWeapon.OnEnemyHitted-=EnemyHitted;
        charge.visual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
        enemy.chargeWeapon.DisableWeapon();
    }
    protected void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        enemy.chargeWeapon.OnEnemyHitted+=EnemyHitted;
        enemy.chargeWeapon.EnableWeapon();
    }
    protected void EnemyHitted(object sender, EventArgs e)
    {
        parent.SwitchState(ChargeStateName.Cooling);
    }
    protected override void HandleHorizontalMomentum()
    {
        enemy.SetHorizontalFrameVelocity(enemy.moveDir.x*enemy.stats.baseMoveStats.dashVelocity);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.Normalize(enemy.GetPlayerPos()-enemy.transform.position);
    }
}
