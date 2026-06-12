using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotAttackState : ClassAttackState
{
    public NotAttackState(ClassAttackStateMachine parent,PlayerController player) : base(parent, PlayerStateName.NotAttacking, player)
    {
        notAllowedActions= new HashSet<string>{};
        notAllowedTransitions = new HashSet<string>{};
    }
    
}
