using System;
using System.Collections.Generic;

public class SkillCastPlayerState : PlayerState
{
    string skillSelected;
    int skillNumberSelected;
    public SkillCastPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.SkillCast, player)
    {
        notAllowedActions= new HashSet<string>{};
        notAllowedTransitions = new HashSet<string>{};
    }
    public override void EntryState()
    {
        skillNumberSelected=player.currentSkillPressed;
        skillSelected=skillNumberSelected==1?player.skillSlot1:player.skillSlot2;
        player.characterClass.HandleSkill(skillSelected);
        notAllowedActions=player.characterClass.skillStateMachine.currentState.notAllowedActions;
        notAllowedTransitions=player.characterClass.skillStateMachine.currentState.notAllowedTransitions;
        player.stats.ConsumeStamina(player.characterClass.GetStaminaSkillCost(skillSelected));
        player.characterClass.skillStateMachine.OnStateChanged+=CheckEndOfSkill;
        if (skillNumberSelected==1)
        {
            GameInput.instance.OnSkill1Helded+=player.characterClass.skillStateMachine.currentState.HabilityButttonRealized;
        }
        else
        {
            GameInput.instance.OnSkill2Helded+=player.characterClass.skillStateMachine.currentState.HabilityButttonRealized;
        }
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
        player.Move();

    }
    private void CheckEndOfSkill(object sender, StateMachine<ClassSkillState>.StateChangeInfo e)
    {
        if (e.newState.stateName == PlayerStateName.NotCastingSkill)
        {
            if (skillNumberSelected==1)
            {
                GameInput.instance.OnSkill1Helded-=e.oldState.HabilityButttonRealized;
            }
            else
            {
                GameInput.instance.OnSkill2Helded-=e.oldState.HabilityButttonRealized;
            }
            parent.SwitchState(player.moveDir.x==0?PlayerStateName.Idle:PlayerStateName.Walk);
            return;
        }
    }

    public override void ExitState()
    {
        player.characterClass.skillStateMachine.OnStateChanged-=CheckEndOfSkill;
        player.characterClass.FinishSkill();
        if (player.skillSlot1 == skillSelected)
        {
            player.OnSkill1EnterCooldown?.Invoke(player, new PlayerController.CooldownCount{cooldown=player.characterClass.skillS[skillSelected].cooldown});
        }
        if(player.skillSlot2 == skillSelected)
        {
            player.OnSkill2EnterCooldown?.Invoke(player, new PlayerController.CooldownCount{cooldown=player.characterClass.skillS[skillSelected].cooldown});
        }
    }

    public override bool CheckTrasitionConditions()
    {
        return parent.AllowsTransition(PlayerStateName.SkillCast) && player.stats.currentStamina>=player.characterClass.GetStaminaSkillCost(player.currentSkillPressed==1?player.skillSlot1:player.skillSlot2) && player.characterClass.CheckSkillTransitionsConditions(player.currentSkillPressed==1?player.skillSlot1:player.skillSlot2);
    }
   
    
}


