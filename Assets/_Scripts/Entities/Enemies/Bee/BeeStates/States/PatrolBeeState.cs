using UnityEngine;

public class PatrolBeeState : PatrolState<BeeController,BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public PatrolBeeState(BeeStateMachine parent, BeeController enemy) : base(parent, enemy)
    {
    }

    public override void EntryState()
    {
        initialPosition=enemy.transform.position;
        float stepX=Random.Range(-enemy.stats.baseStats.patrolRange,enemy.stats.baseStats.patrolRange);
        float stepY=Random.Range(-enemy.stats.baseStats.patrolRangeY,enemy.stats.baseStats.patrolRangeY);
        targetPosition=enemy.wardPosition+stepX*Vector3.right+stepY*Vector3.up;
        Debug.DrawRay(targetPosition,Vector3.down*1);
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        if ((targetPosition-enemy.transform.position).magnitude<0.09f)
        {
            parent.SwitchState(EnemyStateName.Ward);
            return;
        }
        if (DistanceToPlayer()<enemy.stats.baseStats.rangeOfVision)
        {
            parent.SwitchState(EnemyStateName.Chase);
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
}
