using System;
using UnityEngine;

public class ChargeVisual : EnemyVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;
     public override void MainStateChanged(object sender,StateMachine<EnemyState>.StateChangeInfo stateChangeInfo)
    {
        base.MainStateChanged(sender,stateChangeInfo);
        switch (mainStateName)
        {
            case ChargeStateName.Ward:
            animator.Play("Idle");
            break;

            case ChargeStateName.Patrol:
            animator.Play("Walk");
            break;

            case ChargeStateName.Chase:
            animator.Play("Walk");
            break;

            case ChargeStateName.Attack:
            animator.Play("Attack");
            break;

            case ChargeStateName.Hurt:
            animator.Play("Hurt");
            break;
            case ChargeStateName.Cooling:
            animator.Play("Idle");
            break;
            case ChargeStateName.GoBack:
            animator.Play("Walk");
            break;

            default:
            break;
        }
    }
    public void InitiateOfAttackAnimation()
    {
        OnInitiateOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }
}
