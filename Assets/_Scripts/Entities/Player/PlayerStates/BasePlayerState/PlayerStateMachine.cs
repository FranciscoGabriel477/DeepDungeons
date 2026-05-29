using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerState>
{
    public PlayerStateMachine(PlayerController playerController)
    {
        RegisterState(new IdlePlayerState(this,playerController));
        RegisterState(new WalkPlayerState(this,playerController));
        RegisterState(new AttackPlayerState(this,playerController));
        RegisterState(new HurtPlayerState(this,playerController));
        RegisterState(new DashPlayerState(this,playerController));
        RegisterState(new BlockPlayerState(this,playerController));
        SwitchState(PlayerStateName.Idle);
    }

    public bool AllowsJump()
    {
        return !currentState.notAllowedActions.Contains(PlayerActionName.Jump);
    }

    public bool AllowsRotate()
    {
        return !currentState.notAllowedActions.Contains(PlayerActionName.Rotate);
    }
}
