using Unity.VisualScripting;
using UnityEngine;

public class OrcState : EnemyStateWithComponents<OrcController,OrcMover,OrcVisual,OrcStats,OrcStateMachine,OrcBaseStats,OrcBaseMoveStats>
{
    protected OrcController orc;
    public OrcState(OrcStateMachine parent, string stateName,OrcController orc) : base(parent, stateName,orc)
    {
        this.orc=orc;
    }
    
}
