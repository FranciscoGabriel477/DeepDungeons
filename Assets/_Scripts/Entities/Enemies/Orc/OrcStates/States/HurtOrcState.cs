using UnityEngine;

public class HurtOrcState : OrcState
{
    private float actualTime;
    public HurtOrcState(OrcStateMachine parent,OrcController orc) : base(parent, OrcStateName.Hurt, orc){}

    public override void EntryState()
    {
        actualTime=orc.baseStats.KOtime;
        orc.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(OrcStateName.Chase);
            return;
        }
    }
   
}
