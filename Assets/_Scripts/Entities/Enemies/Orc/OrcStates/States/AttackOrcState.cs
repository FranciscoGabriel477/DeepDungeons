using System;
using UnityEngine;

public class AttackOrcState : OrcState
{
    private float actualTime;
    public AttackOrcState(OrcStateMachine parent,OrcController orc) : base(parent, "Attack", orc)
    {
        
    }

    public override void EntryState()
    {
        orc.SetHorizontalFrameVelocity(0);
        actualTime=orc.baseStats.attackTime;
        orc.orcVisual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
        orc.orcVisual.OnEndOfAttackAnimation+=EndOfAttackAnimation;
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState("Chase");
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }
    public override void ExitState()
    {
        orc.orcVisual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
        orc.orcVisual.OnEndOfAttackAnimation-=EndOfAttackAnimation;
    }

    private void EndOfAttackAnimation(object sender, EventArgs e){
        //parent.SwitchState("Chase");
    }
    private void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        orc.orcWeapon.Attack(orc.transform.rotation.eulerAngles.y);
    }
}
