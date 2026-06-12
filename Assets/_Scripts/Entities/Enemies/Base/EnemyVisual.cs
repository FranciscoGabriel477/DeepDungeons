using System;
using System.Collections;
using UnityEngine;

public class EnemyVisual : EntityVisual
{
    private void OnDestroy()
    {
        animator.Play(EnemyStateName.Death);
    }
    public virtual void MainStateChanged(object sender,StateMachine<EnemyState>.StateChangeInfo stateChangeInfo)
    {
        mainStateName=stateChangeInfo.newState.stateName;
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
