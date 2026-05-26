using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : EntityVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;
    private string airStateName;

    private void OnEnable()
    {
        
    }    
    public void InitiateOfAttackAnimation()
    {
        OnInitiateOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }

    //public void EndOfAttackAnimation()
    //{
        //OnEndOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    //}
    public void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo)
    {
        mainStateName=stateChangeInfo.newState.stateName;
        switch (mainStateName)
        {
            case PlayerStateName.Idle:
                animator.Play("Idle");
                break;

            case PlayerStateName.Walk:
                animator.Play("Walk");
                break;

            case PlayerStateName.Attack:
                animator.Play("Attack1");
                break;

            case PlayerStateName.Hurt:
                animator.Play("Hurt");
                break;
            
            default:
                break;
        }
    }
    public void AirStateChanged(object sender,StateMachine<PlayerAirState>.StateChangeInfo stateChangeInfo)
    {
        airStateName=stateChangeInfo.newState.stateName;
    }
}
