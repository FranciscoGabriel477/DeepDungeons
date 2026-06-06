using UnityEngine;

public class FallPlayerState : PlayerAirState
{
    public FallPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,PlayerAirStateName.Fall,player){}
    public override void UpdateState(float deltaTime)
    {        
        if (player.IsGrounded)
        {
            player.SetVerticalFrameVelocity(0);
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
        float newVelocityY=player.frameVelocity.y+player.stats.baseMoveStats.gravityAcc*fixedDeltaTime;
        player.SetVerticalFrameVelocity(newVelocityY);
    }

}
