using UnityEngine;

public class EnemyVisual : EntityVisual
{
    public void OnDestroy()
    {
        animator.Play(EnemyStateName.Death);
    }

    public virtual void MainStateChanged(object sender,StateMachine<EnemyState>.StateChangeInfo stateChangeInfo)
    {
        mainStateName=stateChangeInfo.newState.stateName;
    }
}
