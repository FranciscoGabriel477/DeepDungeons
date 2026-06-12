using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotCastingSkillState : ClassSkillState 
{
    public NotCastingSkillState(ClassSkillStateMachine parent,PlayerController player) : base(parent, PlayerStateName.NotCastingSkill, player)
    {
        notAllowedActions= new HashSet<string>{};
        notAllowedTransitions = new HashSet<string>{};
    }
    
}
