using UnityEngine;

public class FastFallTransitionPlayerState : PlayerAirState
{
    private float currentTimerInFastFallingTrasition;
    private float verticalVelocityInJumpHeld;
    public FastFallTransitionPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,PlayerAirStateName.FastFallTransition,player){}
    public override void EntryState()
    {
        verticalVelocityInJumpHeld=player.frameVelocity.y;
        currentTimerInFastFallingTrasition=player.stats.baseMoveStats.timeInFastFallingTrasition;
    }

    public override void UpdateState(float deltaTime)
    {
        currentTimerInFastFallingTrasition-=deltaTime;
        if (currentTimerInFastFallingTrasition <= 0 || player.IsHeadBump)
        {
            parent.SwitchState(PlayerAirStateName.FastFall);
            return;
        }
        if (player.IsGrounded)
        {
            parent.SwitchState(PlayerAirStateName.NotInAir);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleVerticalMomentum(fixedDeltaTime);
    }

    

    protected override void HandleVerticalMomentum(float fixedDeltaTime)
    {
        float newVelocityY=Mathf.Lerp(verticalVelocityInJumpHeld,0,(player.stats.baseMoveStats.timeInFastFallingTrasition-currentTimerInFastFallingTrasition)/player.stats.baseMoveStats.timeInFastFallingTrasition);
        player.SetVerticalFrameVelocity(newVelocityY);
    }

}
