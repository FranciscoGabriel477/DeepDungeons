using UnityEngine;

public class HurtOrcState : OrcState
{
    public HurtOrcState(OrcStateMachine parent,OrcController orc) : base(parent, "Hurt", orc)
    {
    }

    public override void EntryState()
    {
        orc.orcVisual.PlayAnimation("Hurt");
        
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
}
