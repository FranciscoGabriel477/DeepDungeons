using UnityEngine;

public class WalkPlayerState : PlayerState
{
    public WalkPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent,PlayerStateName.Walk, player){}
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if (player.moveDir.x == 0)
        {
            parent.SwitchState(PlayerStateName.Idle);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
        HandleRotation();
        HandleHorizontalMomentum();
        player.Move();
    }
}
