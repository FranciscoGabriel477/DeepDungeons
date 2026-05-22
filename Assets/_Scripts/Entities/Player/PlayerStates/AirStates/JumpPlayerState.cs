using Unity.VisualScripting;
using UnityEngine;

public class JumpPlayerState : PlayerAirState
{

    public JumpPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent,"Jump",player){}
    public override void EntryState()
    {
        GameInputEnable();
        player.SetVerticalFrameVelocity(player.baseMoveStats.jumpInitialSpeed);
        player.ResetJumpBuffer();
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if(Mathf.InverseLerp(player.baseMoveStats.jumpInitialSpeed,0f,player.frameVelocity.y)>player.baseMoveStats.apexThresHold)
        {
            parent.SwitchState("JumpApex");
            return;
        }

        if(player.jumpIsHelded && player.frameVelocity.y < player.baseMoveStats.minVerticalJumpVelocity)
        {
            parent.SwitchState("FastFallTransition");
            return;
        }

        if(player.frameVelocity.y<0)
        {
            parent.SwitchState("FastFall");
            return;
        }

        if(!player.playerStateMachine.AllowsJump())
        {
            parent.SwitchState("FastFall");
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
        float newVelocityY=player.frameVelocity.y+player.baseMoveStats.gravityAcc*fixedDeltaTime;
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
