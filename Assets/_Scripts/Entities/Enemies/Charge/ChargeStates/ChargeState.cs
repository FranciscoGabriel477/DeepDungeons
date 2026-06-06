using Unity.VisualScripting;
using UnityEngine;

public class ChargeState : EnemyStateWithComponents<ChargeController,ChargeMover,ChargeVisual,ChargeStats,ChargeStateMachine,ChargeBaseStats,ChargeBaseMoveStats>
{
    protected ChargeController charge;
    public ChargeState(ChargeStateMachine parent, string stateName,ChargeController charge) : base(parent, stateName,charge)
    {
        this.charge=charge;
    }
    
}
