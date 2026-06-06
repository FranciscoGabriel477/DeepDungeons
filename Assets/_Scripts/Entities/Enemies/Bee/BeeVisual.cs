using System;
using UnityEngine;

public class BeeVisual : EnemyVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;
     public override void MainStateChanged(object sender,StateMachine<EnemyState>.StateChangeInfo stateChangeInfo)
    {
        base.MainStateChanged(sender,stateChangeInfo);
        switch (mainStateName)
        {
            case BeeStateName.Ward:
            animator.Play("Fly");
            break;

            case BeeStateName.Patrol:
            animator.Play("Fly");
            break;

            case BeeStateName.Chase:
            animator.Play("Fly");
            break;
            
            case BeeStateName.Attack:
            animator.Play("Attack");
            break;

            case BeeStateName.Hurt:
            animator.Play("Hurt");
            break;
            case BeeStateName.Cooling:
            animator.Play("Fly");
            break;
            case BeeStateName.GoBack:
            animator.Play("Fly");
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
