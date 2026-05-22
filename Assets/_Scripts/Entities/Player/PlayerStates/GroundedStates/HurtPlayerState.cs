using UnityEngine;

public class HurtPlayerState : PlayerState
{
    public HurtPlayerState(PlayerStateMachine parent, PlayerController player) : base(parent,"Hurt", player)
    {
        canJump=false;
        canRotate=false;
    }

    public override void EntryState()
    {
        GameInputEnable();
        player.playerVisual.PlayAnimation("Hurt");
    }

    public override void UpdateState(float deltaTime)
    {
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

    protected override void HandleHorizontalMomentum()
    {
        player.SetHorizontalFrameVelocity(player.externalForce.x);
    }
}
