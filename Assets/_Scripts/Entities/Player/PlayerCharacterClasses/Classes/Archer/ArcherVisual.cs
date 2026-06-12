using System;
using UnityEngine;

public class ArcherVisual : PlayerCharacterClassVisual
{
    public event EventHandler OnInitiateOfRangeAttack1Animation;
    public event EventHandler OnInitiateOfToxicArrowSkillAnimation;
    public override void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo)
    {
        if(mainStateName == PlayerStateName.Death)
        {
            return;
        }
        mainStateName=stateChangeInfo.newState.stateName;
        if (mainStateName == PlayerStateName.Death)
        {
            animator.Play(PlayerStateName.Death);
            return;
        }
        if (airStateName!= PlayerAirStateName.NotInAir || attackStateName!= PlayerStateName.NotAttacking || skillStateName!=PlayerStateName.NotCastingSkill)
        {
            switch (mainStateName)
            {
                case PlayerStateName.Hurt:
                    animator.Play(PlayerStateName.Hurt);
                break;
                case PlayerStateName.Dash:
                    if(airStateName!=PlayerAirStateName.NotInAir && attackStateName== PlayerStateName.NotAttacking && skillStateName == PlayerStateName.NotCastingSkill)
                    {
                        animator.Play("AirDash");
                    }
                break;
            }
            return;
        }
        switch (mainStateName)
        {
            case PlayerStateName.Idle:
                animator.Play(PlayerStateName.Idle);
                break;
            case PlayerStateName.Walk:
                animator.Play(PlayerStateName.Walk);
                break;
            case PlayerStateName.Hurt:
                animator.Play(PlayerStateName.Hurt);
                break;
            case PlayerStateName.Dash:
                animator.Play(PlayerStateName.Dash);
                break;
            case PlayerStateName.Block:
                animator.Play(PlayerStateName.Block);
                break;
            default:
                break;
        }
    }
    public void InitiateOfRangeAttack1Animation()
    {
        OnInitiateOfRangeAttack1Animation?.Invoke(this,EventArgs.Empty);
    }
    public void InitiateOfToxicArrowAttackAnimation()
    {
        OnInitiateOfToxicArrowSkillAnimation?.Invoke(this,EventArgs.Empty);
    }
}
