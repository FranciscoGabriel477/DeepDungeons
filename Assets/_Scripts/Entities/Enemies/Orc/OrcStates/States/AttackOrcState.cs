using System;
using UnityEngine;

public class AttackOrcState : OrcState
{
    private float actualTime;
    public AttackOrcState(OrcStateMachine parent,OrcController orc) : base(parent, OrcStateName.Attack, orc){}

    public override void EntryState()
    {
        orc.SetHorizontalFrameVelocity(0);
        actualTime=orc.stats.baseStats.attackTime;
        orc.visual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;

    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(OrcStateName.Cooling);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }
    public override void ExitState()
    {
        orc.visual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
    }   
    private void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        orc.orcWeapon.Attack(orc.transform.rotation.eulerAngles.y);
    }
    protected override void HandleMoveDir()
    {
        orc.moveDir=Vector3.Normalize((orc.GetPlayerPos().x*Vector3.right-orc.transform.position));
    }
}
