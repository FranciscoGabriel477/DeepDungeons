using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerCharacterClassVisual : EntityVisual
{
    protected string airStateName=PlayerAirStateName.NotInAir;
    protected string skillStateName=PlayerStateName.NotCastingSkill;
    protected string attackStateName=PlayerStateName.NotAttacking;
    protected bool airReserved=false;
    public virtual void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo){}
    public virtual void AirStateChanged(object sender,StateMachine<PlayerAirState>.StateChangeInfo stateChangeInfo)
    {
        airStateName=stateChangeInfo.newState.stateName;
        if (airReserved || mainStateName==PlayerStateName.Death)
        {
            return;
        }
        switch(airStateName){
            case PlayerAirStateName.NotInAir:
                animator.Play(mainStateName);
                break;
            case PlayerAirStateName.Jump:
                animator.Play(PlayerAirStateName.Jump);
            break;
            case PlayerAirStateName.FastFall:
                animator.Play(PlayerAirStateName.Fall);
            break;
            case PlayerAirStateName.Fall:
                animator.Play(PlayerAirStateName.Fall);
                break;
            default:
            break;
        }
    }

    public virtual void AttackStateChanged(object sender, StateMachine<ClassAttackState>.StateChangeInfo stateChangeInfo)
    {
        attackStateName=stateChangeInfo.newState.stateName;
        if (attackStateName != PlayerStateName.NotAttacking)
        {
            animator.Play(attackStateName);
        }
    }
    public virtual void SkillStateChanged(object sender, StateMachine<ClassSkillState>.StateChangeInfo stateChangeInfo)
    {
        skillStateName=stateChangeInfo.newState.stateName;
        if (skillStateName != PlayerStateName.NotCastingSkill)
        {
            animator.Play(skillStateName);
        }
    }
    public virtual void DoAirVisualReserve()
    {
        airReserved=true;
    }

    public virtual void CancelAirVisualReserve()
    {
        airReserved=false;
        switch(airStateName){
            case PlayerAirStateName.NotInAir:
                animator.Play(mainStateName);
                break;
            case PlayerAirStateName.Jump:
                animator.Play(PlayerAirStateName.Jump);
            break;
            case PlayerAirStateName.JumpApex:
                animator.Play(PlayerAirStateName.Jump);
            break;
            case PlayerAirStateName.FastFall:
                animator.Play(PlayerAirStateName.Fall);
            break;
            case PlayerAirStateName.FastFallTransition:
                animator.Play(PlayerAirStateName.Fall);
            break;
            case PlayerAirStateName.Fall:
                animator.Play(PlayerAirStateName.Fall);
                break;
            default:
            break;
        }
    }

    public void StartFlahshing(float flashingInterval, float flashingTotalTime)
    {
        StartCoroutine(FlashingAfterDamage(flashingInterval,flashingTotalTime));
    }
    protected IEnumerator FlashingAfterDamage(float flashingInterval, float flashingTotalTime)
    {
        while (flashingTotalTime > 0)
        {
            entitySprite.color=Color.white;
            yield return new WaitForSeconds(flashingInterval);
            flashingTotalTime-=flashingInterval;

            entitySprite.color=Color.red;
            yield return new WaitForSeconds(flashingInterval);
            flashingTotalTime-=flashingInterval;
        }
        entitySprite.color=Color.white;
    }

}
