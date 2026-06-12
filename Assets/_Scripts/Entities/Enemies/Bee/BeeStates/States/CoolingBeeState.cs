using UnityEngine;

public class CoolingBeeState : CoolingState<BeeController,BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public CoolingBeeState(BeeStateMachine parent, BeeController enemy) : base(parent, enemy){}

    public override void EntryState()
    {
        base.EntryState();
        HandleMoveDir();
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
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
        if (enemy.transform.position.y >= enemy.wardPosition.y)
        {
            if (enemy.frameVelocity.y != 0)
            {
                enemy.SetVerticalFrameVelocity(0);
            }
            return;
        }
        enemy.SetVerticalFrameVelocity(enemy.moveDir.y*enemy.stats.baseMoveStats.coolingVelocity);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.up;
    }

}
