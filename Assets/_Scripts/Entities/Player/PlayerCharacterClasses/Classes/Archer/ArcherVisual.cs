using System;
using UnityEngine;

public class ArcherVisual : PlayerCharacterClassVisual
{
    public event EventHandler OnInitiateOfRangeAttack1Animation;
    public override void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo)
    {
        mainStateName=stateChangeInfo.newState.stateName;
        switch (mainStateName)
        {
            case PlayerStateName.Idle:
                animator.Play(PlayerStateName.Idle);
                break;

            case PlayerStateName.Walk:
                animator.Play(PlayerStateName.Walk);
                break;

            case PlayerStateName.Attack:
                animator.Play("Range1");
                break;

            case PlayerStateName.Hurt:
                animator.Play(PlayerStateName.Hurt);
                break;
            
            default:
                animator.Play(PlayerStateName.Idle);
                break;
        }
    }
    public void InitiateOfRangeAttack1Animation()
    {
        OnInitiateOfRangeAttack1Animation?.Invoke(this,EventArgs.Empty);
    }
}
