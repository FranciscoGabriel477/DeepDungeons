using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerState : PlayerState
{
    private float actualTime;
    public HurtPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Hurt, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump,PlayerActionName.Rotate};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block};
    }

    public override void EntryState()
    {
        actualTime=player.stats.baseStats.timeInHurtState;
        player.SetHorizontalFrameVelocity(0);
        if (!player.IsGrounded)
        {
            player.playerAirControlStateMachine.SwitchState(PlayerAirStateName.FastFall);
        }
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
