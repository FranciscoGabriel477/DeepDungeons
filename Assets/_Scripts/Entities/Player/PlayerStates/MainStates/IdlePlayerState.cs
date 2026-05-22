using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public IdlePlayerState(PlayerStateMachine parent,PlayerController player) : base(parent,"Idle",player){}
    public override void EntryState()
    {
        GameInputEnable();
        //player.playerVisual.PlayAnimation("Idle");
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if (player.moveDir.x != 0)
        {
            parent.SwitchState("Walk");
            return;
        }
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
    }

    public override void ExitState()
    {
        GameInputDisable();
    }

}
