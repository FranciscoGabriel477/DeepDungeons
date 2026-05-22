using UnityEngine;

public class FallPlayerState : PlayerAirState
{
    public FallPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,"Fall",player){}
    public override void EntryState()
    {
        GameInputEnable();
        //player.playerVisual.PlayAnimation("Idle");
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        
        if (player.IsGrounded)
        {
            player.SetVerticalFrameVelocity(0);
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
        float newVelocityY=player.frameVelocity.y+player.baseMoveStats.gravityAcc*fixedDeltaTime;
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
