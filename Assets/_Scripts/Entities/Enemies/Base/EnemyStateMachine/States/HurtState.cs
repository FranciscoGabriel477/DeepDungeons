using UnityEngine;

public class HurtState<T, Y, U, I, F, P, Q> : EnemyStateWithComponents<T, Y, U, I, F, P, Q>
    where T : EnemyController<Y, U, I,F, P, Q>
    where Y : EnemyMover
    where U : EnemyVisual
    where I : EnemyStats<P, Q>
    where F:EnemyStateMachine
    where P : EnemyBaseStats
    where Q : EnemyBaseMoveStats
{
    private float actualTime;
    public HurtState(F parent,T enemy) : base(parent, EnemyStateName.Hurt, enemy){}

    public override void EntryState()
    {
        actualTime=enemy.stats.baseStats.timeInHurtState;
        enemy.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            if((DistanceToPlayer() <= enemy.stats.baseStats.chaseRange) && IsBounded())
            {
                parent.SwitchState(EnemyStateName.Chase);
                return;
            }
            else
            {
                parent.SwitchState(EnemyStateName.GoBack);
                return;
            }
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleRotation();
    }
   
}
