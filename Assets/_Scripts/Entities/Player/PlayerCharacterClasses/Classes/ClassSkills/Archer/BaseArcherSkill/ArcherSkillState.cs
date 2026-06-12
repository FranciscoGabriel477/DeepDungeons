using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcherSkillState : ClassSkillState
{
    protected ArcherClass archer;
    public ArcherSkillState(ClassSkillStateMachine parent,string stateName,PlayerController player) : base(parent, stateName,player)
    {
        archer= player.characterClass as ArcherClass;
    }
}
