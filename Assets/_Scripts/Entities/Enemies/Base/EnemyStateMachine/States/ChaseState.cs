using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseState<T, Y, U, I, F, P, Q> : EnemyStateWithComponents<T, Y, U, I, F, P, Q>
    where T : EnemyController<Y, U, I,F, P, Q>
    where Y : EnemyMover
    where U : EnemyVisual
    where I : EnemyStats<P, Q>
    where F:  EnemyStateMachine
    where P : EnemyBaseStats
    where Q : EnemyBaseMoveStats
{
    public ChaseState(F parent,T enemy) : base(parent, EnemyStateName.Chase, enemy){}

    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        /*if (DistanceToPlayer() <= enemy.stats.baseStats.attackRange)
        {
            parent.SwitchState(EnemyStateName.Attack);
            return;
        }*/
        if (enemy.allAttacksOnCooldown)
        {
            parent.SwitchState(EnemyStateName.Cooling);
            return;
        }
        for(int i = 0; i < enemy.stats.baseStats.attackDatas.Count; i++)
        {
            if (DistanceToPlayer() <= enemy.stats.baseStats.attackDatas[i].attackRange && enemy.attacksCooldowns[i]<=0f)
            {
                parent.SwitchState(enemy.stats.baseStats.attackDatas[i].attackName);
                return;
            }
        }
        if((DistanceToPlayer() >= enemy.stats.baseStats.chaseRange) || !IsBounded())
        {
            parent.SwitchState(EnemyStateName.GoBack);
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
        enemy.moveDir=Vector3.Normalize((enemy.GetPlayerPos().x*Vector3.right-enemy.transform.position.x*Vector3.right));
    }
}
