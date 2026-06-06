using UnityEngine;

public class GoBackBeeState : GoBackState<BeeController,BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public GoBackBeeState(BeeStateMachine parent, BeeController enemy) : base(parent, enemy)
    {
    }

    public override void EntryState()
    {
        base.EntryState();
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        if ((targetPosition-enemy.transform.position).magnitude<0.09f)
        {
            parent.SwitchState(EnemyStateName.Ward);
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
