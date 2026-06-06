using Unity.VisualScripting;
using UnityEngine;

public class WardOrcState : WardState<OrcController,OrcMover,OrcVisual,OrcStats,OrcStateMachine,OrcBaseStats,OrcBaseMoveStats>
{
    public WardOrcState(OrcStateMachine parent,OrcController orc) : base(parent, orc){}

    public override void EntryState()
    {
        base.EntryState();
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
    }
}
