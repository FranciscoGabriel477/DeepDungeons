using UnityEngine;

public class WalkOrcState : OrcState
{
    public WalkOrcState(OrcStateMachine parent,OrcController orc) : base(parent, "Walk", orc)
    {
    }

    public override void EntryState()
    {
        orc.orcVisual.PlayAnimation("Walk");
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
        base.FixedUpdateState(fixedDeltaTime);
        if ((orc.GetPlayerPos() - orc.transform.position).magnitude <= orc.baseStats.attackRange)
        {
            //parent.SwitchState("Attack");
        }
    }
    public override void ExitState()
    {
        
    }

    protected override void HandleMoveDir()
    {
        orc.moveDir=orc.GetPlayerPos().x-orc.transform.position.x<0?Vector2.left:Vector2.right;
    }
}
