using UnityEngine;

public class PlayerAirControlStateMachine : StateMachine<PlayerAirState>
{
    public PlayerAirControlStateMachine(PlayerController playerController)
    {
        RegisterState(new NotInAirPlayerState(this,playerController));
        RegisterState(new JumpPlayerState(this,playerController));
        RegisterState(new JumpApexPlayerState(this,playerController));
        RegisterState(new FallPlayerState(this,playerController));
        RegisterState(new FastFallPlayerState(this,playerController));
        RegisterState(new FastFallTransitionPlayerState(this,playerController));
        SwitchState("NotInAir");
    }
}
