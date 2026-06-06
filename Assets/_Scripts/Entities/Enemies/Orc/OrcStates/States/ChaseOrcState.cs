using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseOrcState : ChaseState<OrcController,OrcMover,OrcVisual,OrcStats,OrcStateMachine,OrcBaseStats,OrcBaseMoveStats>
{
    public ChaseOrcState(OrcStateMachine parent,OrcController orc) : base(parent,orc){}

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
    }
    
}
