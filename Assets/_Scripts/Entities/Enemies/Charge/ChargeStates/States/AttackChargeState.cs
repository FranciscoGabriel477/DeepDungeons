using System;
using UnityEngine;

public class AttackChargeState : ChargeState
{
    private float actualTime;
    private int numberOfAttackInList;
    private bool dashStart;
    public AttackChargeState(ChargeStateMachine parent, ChargeController charge) : base(parent, ChargeStateName.Attack, charge){}
    public override void EntryState()
    {
        base.EntryState();
        actualTime=enemy.stats.baseStats.attackDatas[numberOfAttackInList].attackTime;
        charge.visual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
        enemy.SetHorizontalFrameVelocity(0f);
        dashStart=false;
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
        
        if (dashStart)
        {
            HandleHorizontalMomentum();
        }
        else
        {
            HandleRotation();
            HandleMoveDir();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.chargeWeapon.OnEnemyHitted-=EnemyHitted;
        charge.visual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
        enemy.attacksCooldowns[numberOfAttackInList]=enemy.stats.baseStats.attackDatas[numberOfAttackInList].cooldown;
        enemy.chargeWeapon.DisableWeapon(0);
    }
    protected void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        enemy.chargeWeapon.OnEnemyHitted+=EnemyHitted;
        enemy.chargeWeapon.EnableWeapon(0);
        dashStart=true;
        charge.visual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
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
