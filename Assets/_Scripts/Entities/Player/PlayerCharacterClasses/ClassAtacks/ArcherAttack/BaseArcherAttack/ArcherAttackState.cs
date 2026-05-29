using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcherAttackState : ClassAttackState
{
    protected ArcherClass archer;
    public ArcherAttackState(ClassAttackStateMachine parent,string stateName,PlayerController player) : base(parent, stateName,player)
    {
        archer= player.characterClass as ArcherClass;
    }
}
