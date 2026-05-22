using UnityEngine;

public class FastFallPlayerState : PlayerAirState
{
    public FastFallPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,"FastFall",player){}
    public override void EntryState()
    {
        GameInputEnable();
       // player.playerVisual.PlayAnimation("Idle");
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if (player.IsGrounded)
        {
            parent.SwitchState("NotInAir");
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleVerticalMomentum(fixedDeltaTime);
    }

    public override void ExitState()
    {
        GameInputDisable();
    }
    protected override void HandleVerticalMomentum(float fixedDeltaTime)
    {
        float newVelocityY=player.frameVelocity.y+player.baseMoveStats.gravityAcc*fixedDeltaTime*player.baseMoveStats.gravityMultiplierOnJumpRelease;
        player.SetVerticalFrameVelocity(newVelocityY);
    }

    protected override void GameInputEnable()
    {
        gameInput.OnJumpPressed+=player.JumpPressed;
        gameInput.OnJumpHelded+=player.JumpHelded;
    }

    protected override void GameInputDisable()
    {
        gameInput.OnJumpPressed-=player.JumpPressed;
        gameInput.OnJumpHelded-=player.JumpHelded;
    }
}
