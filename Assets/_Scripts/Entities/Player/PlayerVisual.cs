using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : EntityVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;
    public event EventHandler OnEndOfAttackAnimation;
    private string airStateName;

    private void OnEnable()
    {
        
    }    
    public void InitiateOfAttackAnimation()
    {
        OnInitiateOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }

    public void EndOfAttackAnimation()
    {
        OnEndOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }
    public void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo)
    {
        mainStateName=stateChangeInfo.newState.stateName;
        switch (mainStateName)
        {
            case "Idle":
            animator.Play("Idle");
            break;

            case "Walk":
            animator.Play("Walk");
            break;

            case "Attack":
            animator.Play("Attack1");
            break;

            case "Hurt":
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
