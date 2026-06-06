using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseChargeState : ChaseState<ChargeController,ChargeMover,ChargeVisual,ChargeStats,ChargeStateMachine,ChargeBaseStats,ChargeBaseMoveStats>
{
    public ChaseChargeState(ChargeStateMachine parent,ChargeController charge) : base(parent,charge){}

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
    }
    
}
