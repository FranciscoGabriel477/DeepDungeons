using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CoolingChargeState: CoolingState<ChargeController,ChargeMover,ChargeVisual,ChargeStats,ChargeStateMachine,ChargeBaseStats,ChargeBaseMoveStats>
{
    public CoolingChargeState(ChargeStateMachine parent,ChargeController charge) : base(parent,charge){}

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
