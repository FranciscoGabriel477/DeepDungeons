using Unity.VisualScripting;
using UnityEngine;

public class WardState<T, Y, U, I, F, P, Q> : EnemyStateWithComponents<T, Y, U, I, F, P, Q>
    where T : EnemyController<Y, U, I,F, P, Q>
    where Y : EnemyMover
    where U : EnemyVisual
    where I : EnemyStats<P, Q>
    where F:  EnemyStateMachine
    where P : EnemyBaseStats
    where Q : EnemyBaseMoveStats
{
    private float currentTimeOnWard;
    public WardState(F parent,T enemy) : base(parent, EnemyStateName.Ward, enemy){}

    public override void EntryState()
    {
        currentTimeOnWard=Random.Range(enemy.stats.baseStats.minWardTime,enemy.stats.baseStats.maxWardTime);
        enemy.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        currentTimeOnWard-=deltaTime;
        if (currentTimeOnWard <= 0)
        {
            parent.SwitchState(EnemyStateName.Patrol);
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
        HandleRotation();
    }
}
