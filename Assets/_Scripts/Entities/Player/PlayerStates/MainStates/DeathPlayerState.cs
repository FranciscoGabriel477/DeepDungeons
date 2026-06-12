using System.Collections.Generic;
using UnityEngine;

public class DeathPlayerState : PlayerState
{
    public DeathPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Death, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump,PlayerActionName.Rotate};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block,PlayerStateName.SkillCast,PlayerStateName.Hurt,PlayerStateName.Idle,PlayerStateName.Walk};
    }

    public override void EntryState()
    {
        player.playerHitBox.enabled=false;
    }
    
}