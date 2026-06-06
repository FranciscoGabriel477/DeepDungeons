using Unity.VisualScripting;
using UnityEngine;

public class GoBackState<T, Y, U, I, F, P, Q> : EnemyStateWithComponents<T, Y, U, I, F, P, Q>
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
    public GoBackState(F parent,T enemy) : base(parent, EnemyStateName.GoBack, enemy){}

    public override void EntryState()
    {
        initialPosition=enemy.transform.position;
        targetPosition=enemy.wardPosition;
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        if (Mathf.InverseLerp(initialPosition.x,targetPosition.x,enemy.transform.position.x)>0.97f)
        {
            parent.SwitchState(EnemyStateName.Ward);
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
