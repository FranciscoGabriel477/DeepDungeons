using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierAttackState : ClassAttackState
{
    protected SoldierClass soldier;
    public SoldierAttackState(ClassAttackStateMachine parent,string stateName,PlayerController player) : base(parent, stateName,player)
    {
        soldier= player.characterClass as SoldierClass;
    }
}
