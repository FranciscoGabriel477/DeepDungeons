using System;
using UnityEngine;

public class AttackBeeState : BeeState
{
    float actualTime;
    public AttackBeeState(BeeStateMachine parent, BeeController bee) : base(parent, BeeStateName.Attack, bee)
    {
    }
    public override void EntryState()
    {
        base.EntryState();
        actualTime=enemy.stats.baseStats.attackTime;
        bee.visual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
        HandleMoveDir();
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (enemy.IsGrounded)
        {
            parent.SwitchState(BeeStateName.Cooling);
            return;
        }
        if (actualTime <= 0)
        {
            parent.SwitchState(BeeStateName.Cooling);
            return;
        }
        if (!IsBounded())
        {
            parent.SwitchState(BeeStateName.GoBack);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
       HandleHorizontalMomentum();
       HandleVerticalMomentum(fixedDeltaTime);
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.beeWeapon.OnEnemyHitted-=EnemyHitted;
        bee.visual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
        enemy.beeWeapon.DisableWeapon();
    }
    protected void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        enemy.beeWeapon.OnEnemyHitted+=EnemyHitted;
        enemy.beeWeapon.EnableWeapon();
    }
    protected void EnemyHitted(object sender, EventArgs e)
    {
        parent.SwitchState(BeeStateName.Cooling);
    }
    protected override void HandleHorizontalMomentum()
    {
        enemy.SetHorizontalFrameVelocity(enemy.moveDir.x*enemy.stats.baseMoveStats.dashVelocity);
    }
    protected override void HandleVerticalMomentum(float fixedDeltaTime)
    {
        enemy.SetVerticalFrameVelocity(enemy.moveDir.y*enemy.stats.baseMoveStats.dashVelocity);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.Normalize(enemy.GetPlayerPos()-enemy.transform.position);
    }
}
