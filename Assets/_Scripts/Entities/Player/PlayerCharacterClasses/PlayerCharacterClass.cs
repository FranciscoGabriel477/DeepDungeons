using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacterClass : MonoBehaviour
{
    public PlayerController playerController;
    public Animator classAnimator{get; protected set;}
    public ClassAttackStateMachine attackStateMachine{get; protected set;}
    public ClassSkillStateMachine skillStateMachine{get; protected set;}
    public Dictionary<string,SkillInfo> skillS{get; protected set;}
    
    protected virtual void Update()
    {
        attackStateMachine.Action(Time.deltaTime);
        skillStateMachine.Action(Time.deltaTime);
    }
    protected virtual void FixedUpdate()
    {
        attackStateMachine.FixedAction(Time.fixedDeltaTime);
        skillStateMachine.FixedAction(Time.fixedDeltaTime);

    }
    public virtual void HandleAttack(){}
    public virtual void FinishAttack()
    {
        if (attackStateMachine.GetActualStateName() != PlayerStateName.NotAttacking)
        {
            attackStateMachine.SwitchState(PlayerStateName.NotAttacking);
        }
    }
    public virtual void HandleSkill(string skillName)
    {
        if (skillStateMachine.GetActualStateName() == PlayerStateName.NotCastingSkill)
        {
            skillStateMachine.SwitchState(skillName);
        }
    }

    public virtual bool CheckSkillTransitionsConditions(string skillName)
    {
        return skillStateMachine.GetState(skillName).CheckTrasitionConditions();
    }
    public virtual void FinishSkill()
    {
        if (skillStateMachine.GetActualStateName() != PlayerStateName.NotCastingSkill)
        {
            skillStateMachine.SwitchState(PlayerStateName.NotCastingSkill);
        }
    }

    public abstract float GetStaminaAttackCost();
    public float GetStaminaSkillCost(string skillName)
    {
        return skillS[skillName].staminaCost;
    }
    public abstract void SetupSkillsDictionary();
}
