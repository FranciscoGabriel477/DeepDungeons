using Unity.VisualScripting;
using UnityEngine;

public class JumpPlayerState : PlayerAirState
{

    public JumpPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,PlayerAirStateName.Jump,player){}
    public override void EntryState()
    {
        player.SetVerticalFrameVelocity(player.stats.baseMoveStats.jumpInitialSpeed);
        player.ResetJumpBuffer();
    }

    public override void UpdateState(float deltaTime)
    {
        if(Mathf.InverseLerp(player.stats.baseMoveStats.jumpInitialSpeed,0f,player.frameVelocity.y)>player.stats.baseMoveStats.apexThresHold)
        {
            parent.SwitchState(PlayerAirStateName.JumpApex);
            return;
        }

        if(player.jumpIsHelded && player.frameVelocity.y < player.stats.baseMoveStats.minVerticalJumpVelocity)
        {
            parent.SwitchState(PlayerAirStateName.FastFallTransition);
            return;
        }

        if(player.frameVelocity.y<0)
        {
            parent.SwitchState(PlayerAirStateName.FastFall);
            return;
        }

        /*if(!player.playerStateMachine.AllowsJump())
        {
            parent.SwitchState(PlayerAirStateName.FastFall);
            return;
        }*/


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
