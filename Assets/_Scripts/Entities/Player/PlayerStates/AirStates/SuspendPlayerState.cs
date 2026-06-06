using UnityEngine;

public class SuspendPlayerState : PlayerAirState
{
    public SuspendPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,PlayerAirStateName.Suspend,player){}
    public override void EntryState()
    {
        player.SetVerticalFrameVelocity(0f);

    }

}
