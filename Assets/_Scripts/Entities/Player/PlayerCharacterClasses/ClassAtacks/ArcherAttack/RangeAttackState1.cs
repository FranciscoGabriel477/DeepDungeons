using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeAttackState1 : ArcherAttackState
{
    float actualTime;
    public RangeAttackState1(ClassAttackStateMachine parent,PlayerController player) : base(parent, ArcherAttackName.RangeAttack1, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump,PlayerActionName.Rotate};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block};
    }
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        actualTime=archer.weaponInfo.attackTimeOnRange1;
        archer.archerVisual.OnInitiateOfRangeAttack1Animation+=InitiateRangeAttack1;
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState("NotAttack");
        }
    }
    public override void ExitState()
    {
        archer.archerVisual.OnInitiateOfRangeAttack1Animation-=InitiateRangeAttack1;
    }
    public void InitiateRangeAttack1(object sender, EventArgs e)
    {
        Attack(player.transform.rotation.eulerAngles.y);
    }
    public virtual void Attack(float dir)
    {
        archer.SummomArrow(dir);
    }
}
