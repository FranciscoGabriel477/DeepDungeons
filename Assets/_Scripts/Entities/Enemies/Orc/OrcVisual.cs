using System;
using UnityEngine;

public class OrcVisual : EntityVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;
    public event EventHandler OnEndOfAttackAnimation;
    public void MainStateChanged(object sender,StateMachine<OrcState>.StateChangeInfo stateChangeInfo)
    {
        mainStateName=stateChangeInfo.newState.stateName;
        switch (mainStateName)
        {
            case "Ward":
            animator.Play("Idle");
            break;

            case "Patrol":
            animator.Play("Walk");
            break;

            case "Chase":
            animator.Play("Walk");
            break;

            case "Attack":
            animator.Play("Attack");
            break;

            case "Hurt":
            animator.Play("Hurt");
            break;

            default:
            break;
        }
    }
    public void InitiateOfAttackAnimation()
    {
        OnInitiateOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }

    public void EndOfAttackAnimation()
    {
        OnEndOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }
    
}
