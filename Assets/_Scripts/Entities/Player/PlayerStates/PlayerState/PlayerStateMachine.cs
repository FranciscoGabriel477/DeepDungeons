using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerState>
{
    public PlayerStateMachine(PlayerController playerController)
    {
        RegisterState(new IdlePlayerState(this,playerController));
        RegisterState(new WalkPlayerState(this,playerController));
        RegisterState(new AttackPlayerState(this,playerController));
        SwitchState("Idle");
    }

    public bool AllowsJump()
    {
        return currentState.canJump;
    }

    public bool AllowsRotate()
    {
        return currentState.canRotate;
    }
}
