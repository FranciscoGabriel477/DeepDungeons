using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : State<PlayerAirState>
{
    protected PlayerController player;
    protected GameInput gameInput;
    public PlayerAirState(PlayerAirControlStateMachine parent, string stateName,PlayerController player) : base(parent, stateName)
    {
        this.player=player;
        gameInput=GameInput.instance;
    }
    public override void EntryState(){}
    public override void UpdateState(float deltaTime)
    {
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }
    public override void ExitState(){}
    protected virtual void HandleVerticalMomentum(float fixedDeltaTime)
    {
        
    }
    protected virtual void GameInputEnable()
    {
        gameInput.OnJumpPressed+=player.JumpPressed;
        gameInput.OnJumpHelded+=player.JumpHelded;
    }

    protected virtual void GameInputDisable()
    {
        gameInput.OnJumpPressed-=player.JumpPressed;
        gameInput.OnJumpHelded-=player.JumpHelded;
    }
}
