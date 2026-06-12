using System;
using System.Collections.Generic;

public class AttackPlayerState : PlayerState
{
    public AttackPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Attack, player)
    {
        notAllowedActions= new HashSet<string>{};
        notAllowedTransitions = new HashSet<string>{};
    }
    
    public override void EntryState()
    {
        player.characterClass.HandleAttack();
        player.characterClass.attackStateMachine.OnStateChanged+=CheckEndOfAttack;
        notAllowedActions=player.characterClass.attackStateMachine.currentState.notAllowedActions;
        notAllowedTransitions=player.characterClass.attackStateMachine.currentState.notAllowedTransitions;
        player.stats.ConsumeStamina(player.characterClass.GetStaminaAttackCost());
    }

    private void CheckEndOfAttack(object sender, StateMachine<ClassAttackState>.StateChangeInfo e)
    {
        if (e.newState.stateName == PlayerStateName.NotAttacking)
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
        //return player.IsGrounded && player.playerAirControlStateMachine.GetActualStateName()== PlayerAirStateName.NotInAir && parent.AllowsTransition(PlayerStateName.Attack) && player.stats.currentStamina>=player.characterClass.GetStaminaAttackCost();
        return parent.AllowsTransition(PlayerStateName.Attack) && player.stats.currentStamina>=player.characterClass.GetStaminaAttackCost();
    }
   
    
}


