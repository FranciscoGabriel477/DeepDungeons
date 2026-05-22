using Unity.VisualScripting;
using UnityEngine;

public class WardOrcState : OrcState
{
    private float currentTimeOnWard;
    public WardOrcState(OrcStateMachine parent,OrcController orc) : base(parent, "Ward", orc){}

    public override void EntryState()
    {
        currentTimeOnWard=Random.Range(orc.baseStats.minWardTime,orc.baseStats.maxWardTime);
        orc.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        currentTimeOnWard-=deltaTime;
        if (currentTimeOnWard <= 0)
        {
            parent.SwitchState("Patrol");
            return;
        }
        if ((orc.GetPlayerPos()-orc.transform.position).magnitude<orc.baseStats.rangeOfVision)
        {
            parent.SwitchState("Chase");
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
    }
    public override void ExitState()
    {
        
    }
}
