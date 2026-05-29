using System;
using System.Collections.Generic;

public class AttackPlayerState : PlayerState
{
    public AttackPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Attack, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump,PlayerActionName.Rotate};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block};
    }
    
    public override void EntryState()
    {
        player.characterClass.HandleAttack();
        player.characterClass.attackStateMachine.OnStateChanged+=CheckEndOfAttack;
    }

    private void CheckEndOfAttack(object sender, StateMachine<ClassAttackState>.StateChangeInfo e)
    {
        if (e.newState.stateName == "NotAttack")
        {
            parent.SwitchState(player.moveDir.x==0?PlayerStateName.Idle:PlayerStateName.Walk);
            return;
        }
    }

    public override void ExitState()
    {
        player.characterClass.attackStateMachine.OnStateChanged-=CheckEndOfAttack;
        player.characterClass.FinishAttack();
    }

    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded && player.playerAirControlStateMachine.GetActualStateName()== PlayerAirStateName.NotInAir && parent.AllowsTransition(PlayerStateName.Attack);
    }
   
    
}


