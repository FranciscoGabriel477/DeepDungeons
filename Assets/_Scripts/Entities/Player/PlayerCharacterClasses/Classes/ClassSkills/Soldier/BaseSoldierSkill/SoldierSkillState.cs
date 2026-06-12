using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierSkillState : ClassSkillState
{
    protected SoldierClass soldier;
    public SoldierSkillState(ClassSkillStateMachine parent,string stateName,PlayerController player) : base(parent, stateName,player)
    {
        soldier= player.characterClass as SoldierClass;
    }
}
