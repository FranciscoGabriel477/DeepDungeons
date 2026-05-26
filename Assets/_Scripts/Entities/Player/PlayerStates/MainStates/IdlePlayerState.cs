using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public IdlePlayerState(PlayerStateMachine parent,PlayerController player) : base(parent,PlayerStateName.Idle,player){}

    public override void UpdateState(float deltaTime)
    {
        if (player.moveDir.x != 0)
        {
            parent.SwitchState(PlayerStateName.Walk);
            return;
        }
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
    }
}
