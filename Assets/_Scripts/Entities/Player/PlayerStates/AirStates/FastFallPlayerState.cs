using UnityEngine;

public class FastFallPlayerState : PlayerAirState
{
    public FastFallPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,PlayerAirStateName.FastFall,player){}
    public override void UpdateState(float deltaTime)
    {
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
        float newVelocityY=player.frameVelocity.y+player.baseMoveStats.gravityAcc*fixedDeltaTime*player.baseMoveStats.gravityMultiplierOnJumpRelease;
        player.SetVerticalFrameVelocity(newVelocityY);
    }
}
