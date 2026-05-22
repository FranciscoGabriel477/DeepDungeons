using UnityEngine;

public class JumpApexPlayerState : PlayerAirState
{
    public JumpApexPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,"JumpApex",player){}
    public override void EntryState()
    {
        GameInputEnable();
        player.SetVerticalFrameVelocity(0f);
        TimerHandler.CreateTimer(EndOfApexTime,player.baseMoveStats.timeInApexPoint,"ApexTimer");
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if(player.jumpIsHelded)
        {
            TimerHandler.StopTimer("ApexTimer");
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

    }

    public override void ExitState()
    {
        GameInputDisable();
    }

    private void EndOfApexTime()
    {
        parent.SwitchState("FastFall");
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
