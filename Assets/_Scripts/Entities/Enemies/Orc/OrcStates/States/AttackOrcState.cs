using UnityEngine;

public class AttackOrcState : OrcState
{
    public AttackOrcState(OrcStateMachine parent,OrcController orc) : base(parent, "Attack", orc)
    {
        
    }

    public override void EntryState()
    {
        orc.SetHorizontalFrameVelocity(0);
        orc.orcVisual.PlayAnimation("Attack");        
    }
    public override void UpdateState(float deltaTime)
    {
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
    }
    public override void ExitState()
    {
        
    }
}
