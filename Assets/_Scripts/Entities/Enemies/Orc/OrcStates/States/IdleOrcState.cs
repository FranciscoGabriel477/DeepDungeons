using UnityEngine;

public class IdleOrcState : OrcState
{
    public IdleOrcState(OrcStateMachine parent,OrcController orc) : base(parent, "Idle", orc){}

    public override void EntryState()
    {
        orc.orcVisual.PlayAnimation("Idle");
        orc.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        if ((orc.GetPlayerPos()-orc.transform.position).magnitude<orc.baseStats.rangeOfVision)
        {
            parent.SwitchState("Walk");
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
    }
    public override void ExitState()
    {
        
    }
}
