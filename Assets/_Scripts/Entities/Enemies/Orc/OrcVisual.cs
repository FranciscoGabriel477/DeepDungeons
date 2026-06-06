using System;
using UnityEngine;

public class OrcVisual : EnemyVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;

    public override void MainStateChanged(object sender,StateMachine<EnemyState>.StateChangeInfo stateChangeInfo)
    {
        base.MainStateChanged(sender,stateChangeInfo);
        switch (mainStateName)
        {
            case OrcStateName.Ward:
            animator.Play("Idle");
            break;

            case OrcStateName.Patrol:
            animator.Play("Walk");
            break;

            case OrcStateName.Chase:
            animator.Play("Walk");
            break;

            case OrcStateName.Attack:
            animator.Play("Attack");
            break;

            case OrcStateName.Hurt:
            animator.Play("Hurt");
            break;
            case OrcStateName.Cooling:
            animator.Play("Idle");
            break;
            case OrcStateName.GoBack:
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
