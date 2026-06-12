using UnityEngine;

public class JumpApexPlayerState : PlayerAirState
{
    public JumpApexPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,PlayerAirStateName.JumpApex,player){}
    public override void EntryState()
    {
        player.SetVerticalFrameVelocity(0f);
        TimerHandler.CreateTimer(EndOfApexTime,player.stats.baseMoveStats.timeInApexPoint,"ApexTimer");
    }

    public override void UpdateState(float deltaTime)
    {
        if(player.jumpIsHelded || player.IsHeadBump)
        {
            TimerHandler.StopTimer("ApexTimer");
            parent.SwitchState(PlayerAirStateName.FastFall);
            return;
        }
        if (player.IsGrounded)
        {
            TimerHandler.StopTimer("ApexTimer");
            parent.SwitchState(PlayerAirStateName.NotInAir);
            return;
        }
    }
    private void EndOfApexTime()
    {
        parent.SwitchState(PlayerAirStateName.FastFall);
    }
}
