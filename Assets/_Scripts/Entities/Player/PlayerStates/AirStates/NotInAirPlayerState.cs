using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NotInAirPlayerState : PlayerAirState
{
    
    public NotInAirPlayerState(PlayerAirControlStateMachine parent,PlayerController player) : base(parent, "NotInAir",player)
    {
        this.player=player;
        gameInput=GameInput.instance;
    }
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0f);
        GameInputEnable();
    }
    public override void UpdateState(float deltaTime)
    {
        if (!player.IsGrounded)
        {
            Debug.Log("Yeah");
            parent.SwitchState("Fall");
            return;
        }

        if (player.CheckJumpConditions())
        {
            parent.SwitchState("Jump");
            return;
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
