using Unity.VisualScripting;
using UnityEngine;

public class NotInAirPlayerState : PlayerAirState
{
    
    public NotInAirPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent, PlayerAirStateName.NotInAir,player){}
    public override void EntryState()
    {
        player.SetVerticalFrameVelocity(0f);
    }
    public override void UpdateState(float deltaTime)
    {

        if (player.CheckJumpConditions())
        {
            parent.SwitchState(PlayerAirStateName.Jump);
            return;
        }
        
        if (!player.IsGrounded)
        {
            parent.SwitchState(PlayerAirStateName.Fall);
            return;
        }
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleVerticalMomentum(fixedDeltaTime);
    }
}
