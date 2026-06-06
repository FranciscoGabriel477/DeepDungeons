using UnityEngine;

public class HurtBeeState : HurtState<BeeController,BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public HurtBeeState(BeeStateMachine parent, BeeController enemy) : base(parent, enemy)
    {
    }

    public override void EntryState()
    {
        base.EntryState();
        enemy.SetVerticalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
