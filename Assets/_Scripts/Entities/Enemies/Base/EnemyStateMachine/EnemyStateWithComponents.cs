using Unity.Mathematics;
using UnityEngine;

public abstract class EnemyStateWithComponents<T, Y, U, I, F, P, Q> : EnemyState
    where T : EnemyController<Y, U, I,F, P, Q>
    where Y : EnemyMover
    where U : EnemyVisual
    where I : EnemyStats<P, Q>
    where F : EnemyStateMachine
    where P : EnemyBaseStats
    where Q : EnemyBaseMoveStats
{
    protected T enemy;


    public EnemyStateWithComponents(EnemyStateMachine parent, string stateName, T enemy) : base(parent, stateName)
    {
        this.enemy = enemy;
    }
    protected virtual void HandleHorizontalMomentum()
    {
        enemy.SetHorizontalFrameVelocity(enemy.moveDir.x*enemy.stats.baseMoveStats.moveHorizontalSpeed);
    }
    protected virtual void HandleVerticalMomentum(float fixedDeltaTime){}
    protected virtual void HandleMoveDir(){}
    protected virtual void HandleRotation()
    {
        if (enemy.isFacingRight && enemy.moveDir.x<0)
        {
            enemy.isFacingRight=false;
            enemy.transform.Rotate(0,-180f,0);
        }
        else if(!enemy.isFacingRight && enemy.moveDir.x>0)
        {
            enemy.isFacingRight=true;
            enemy.transform.Rotate(0,180f,0);
        }
    }

    protected virtual bool IsBounded()
    {
        return  (math.abs((enemy.transform.position-enemy.wardPosition).x)<=enemy.maxDistanceX)&&(math.abs((enemy.transform.position-enemy.wardPosition).y)<=enemy.maxDistanceY);
    }

    protected virtual float DistanceToPlayer()
    {
        return (enemy.GetPlayerPos() - enemy.transform.position).magnitude;
    }
}