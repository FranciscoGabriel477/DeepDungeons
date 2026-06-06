using UnityEngine;

public class ChaseBeeState : ChaseState<BeeController,BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public ChaseBeeState(BeeStateMachine parent, BeeController enemy) : base(parent, enemy){}

    public override void EntryState()
    {
        base.EntryState();
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        if (DistanceToPlayer() <= enemy.stats.baseStats.attackRange)
        {
            parent.SwitchState(EnemyStateName.Attack);
            return;
        }
        if((DistanceToPlayer() >= enemy.stats.baseStats.chaseRange) || !IsBounded())
        {
            parent.SwitchState(EnemyStateName.GoBack);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
       HandleVerticalMomentum(fixedDeltaTime);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    protected override void HandleVerticalMomentum(float fixedDeltaTime)
    {
        enemy.SetVerticalFrameVelocity(enemy.moveDir.y*enemy.stats.baseMoveStats.moveHorizontalSpeed);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.Normalize(enemy.GetPlayerPos()+(enemy.stats.baseStats.targetPointAtPlayer*Vector3.up)-enemy.transform.position);
    }
}
