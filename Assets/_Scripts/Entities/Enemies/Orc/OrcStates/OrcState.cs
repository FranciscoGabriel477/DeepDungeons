using Unity.VisualScripting;
using UnityEngine;

public class OrcState : State<OrcState>
{
    protected OrcController orc;
    public bool canRotate;
    public OrcState(OrcStateMachine parent, string stateName,OrcController orc) : base(parent, stateName)
    {
        this.orc=orc;
    }

    public override void EntryState()
    {
        
    }
    public override void UpdateState(float deltaTime)
    {
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
    }

    public override void ExitState()
    {
        
    }


    protected virtual void HandleHorizontalMomentum()
    {
        orc.SetHorizontalFrameVelocity(orc.moveDir.x*orc.baseStats.moveHorizontalSpeed);
    }
    protected virtual void HandleVerticalMomentum(float fixedDeltaTime)
    {
        
    }
    protected virtual void HandleMoveDir()
    {
    }
}
