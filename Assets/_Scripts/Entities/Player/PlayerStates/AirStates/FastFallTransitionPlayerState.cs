using UnityEngine;

public class FastFallTransitionPlayerState : PlayerAirState
{
    private float currentTimerInFastFallingTrasition;
    private float verticalVelocityInJumpHeld;
    public FastFallTransitionPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,"FastFallTransition",player){}
    public override void EntryState()
    {
        GameInputEnable();
        verticalVelocityInJumpHeld=player.frameVelocity.y;
        currentTimerInFastFallingTrasition=player.baseMoveStats.timeInFastFallingTrasition;
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        currentTimerInFastFallingTrasition-=deltaTime;
        if (currentTimerInFastFallingTrasition <= 0)
        {
            parent.SwitchState("FastFall");
            return;
        }
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
        float newVelocityY=Mathf.Lerp(verticalVelocityInJumpHeld,0,(player.baseMoveStats.timeInFastFallingTrasition-currentTimerInFastFallingTrasition)/player.baseMoveStats.timeInFastFallingTrasition);
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
