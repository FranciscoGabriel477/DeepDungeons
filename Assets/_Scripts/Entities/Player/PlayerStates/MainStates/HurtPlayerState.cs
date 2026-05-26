using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerState : PlayerState
{
    private float actualTime;
    public HurtPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Hurt, player)
    {
        canJump=false;
        canRotate=false;
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack};
    }

    public override void EntryState()
    {
        actualTime=player.baseStats.timeInHurtState;
        player.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(player.moveDir.x==0?PlayerStateName.Idle:PlayerStateName.Walk);
            return;
        }
    }
}
