using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CoolingState<T, Y, U, I, F, P, Q> : EnemyStateWithComponents<T, Y, U, I, F, P, Q>
    where T : EnemyController<Y, U, I,F, P, Q>
    where Y : EnemyMover
    where U : EnemyVisual
    where I : EnemyStats<P, Q>
    where F:  EnemyStateMachine
    where P : EnemyBaseStats
    where Q : EnemyBaseMoveStats
{
    public CoolingState(F parent,T enemy) : base(parent, EnemyStateName.Cooling, enemy){}
    float actualTime;
    public override void EntryState()
    {
        enemy.SetHorizontalFrameVelocity(0);
        actualTime=enemy.stats.baseStats.coolingTime;
    }

    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            /*if (DistanceToPlayer() <= enemy.stats.baseStats.attackRange)
            {
                parent.SwitchState(EnemyStateName.Attack);
                return;
            }
            else */
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

    protected override void HandleRotation()
    {
        Vector3 playerDir=(enemy.GetPlayerPos()-enemy.transform.position).normalized;
        if (enemy.isFacingRight && playerDir.x<0)
        {
            enemy.isFacingRight=false;
            enemy.transform.Rotate(0,-180f,0);
        }
        else if(!enemy.isFacingRight && playerDir.x>0)
        {
            enemy.isFacingRight=true;
            enemy.transform.Rotate(0,180f,0);
        }
    }
}
