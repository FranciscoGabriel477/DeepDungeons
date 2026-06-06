using UnityEngine;

public abstract class PlayerCharacterClass : MonoBehaviour
{
    public PlayerController playerController;
    public Animator classAnimator{get; protected set;}
    public ClassAttackStateMachine attackStateMachine{get; protected set;}
    
    public virtual void HandleAttack(){}
    public virtual void FinishAttack()
    {
        if (attackStateMachine.GetActualStateName() != "NotAttack")
        {
            attackStateMachine.SwitchState("NotAttack");
        }
    }

    public abstract float GetStaminaAttackCost();
}
