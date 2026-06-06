using UnityEngine;

public abstract class PatrolState<T, Y, U, I, F, P, Q> : EnemyStateWithComponents<T, Y, U, I, F, P, Q>
    where T : EnemyController<Y, U, I,F, P, Q>
    where Y : EnemyMover
    where U : EnemyVisual
    where I : EnemyStats<P, Q>
    where F:EnemyStateMachine
    where P : EnemyBaseStats
    where Q : EnemyBaseMoveStats
{
    protected Vector3 targetPosition;
    protected Vector3 initialPosition;
    public PatrolState(F parent, T enemy) : base(parent,EnemyStateName.Patrol,enemy)
    {
        
    }
    
    public override void EntryState()
    {
        initialPosition=enemy.transform.position;
        targetPosition=enemy.wardPosition+Random.Range(-enemy.stats.baseStats.patrolRange,enemy.stats.baseStats.patrolRange)*Vector3.right;
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        if (Mathf.InverseLerp(initialPosition.x,targetPosition.x,enemy.transform.position.x)>0.97f)
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
        HandleHorizontalMomentum();
        HandleRotation();
    }
    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.Normalize(targetPosition-enemy.transform.position);
    }

}