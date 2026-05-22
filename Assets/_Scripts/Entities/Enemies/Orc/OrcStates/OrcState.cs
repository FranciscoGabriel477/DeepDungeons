using Unity.VisualScripting;
using UnityEngine;

public class OrcState : State<OrcState>
{
    protected OrcController orc;
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
        orc.Move();
        HandleRotation();
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
    protected virtual void HandleRotation()
    {
        if (orc.isFacingRight && orc.moveDir.x<0)
        {
            orc.isFacingRight=false;
            orc.transform.Rotate(0,-180f,0);
        }
        else if(!orc.isFacingRight && orc.moveDir.x>0)
        {
            orc.isFacingRight=true;
            orc.transform.Rotate(0,180f,0);
        }
    }

    protected virtual void HandleMoveDir()
    {
    }
}
