using Unity.VisualScripting;
using UnityEngine;

public class ChaseOrcState : OrcState
{
    public ChaseOrcState(OrcStateMachine parent,OrcController orc) : base(parent, OrcStateName.Chase, orc){}

    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
        base.FixedUpdateState(fixedDeltaTime);
        if ((orc.GetPlayerPos() - orc.transform.position).magnitude <= orc.baseStats.attackRange)
        {
            parent.SwitchState(OrcStateName.Attack);
            return;
        }
        if((orc.GetPlayerPos() - orc.transform.position).magnitude >= orc.baseStats.chaseRange)
        {
            parent.SwitchState(OrcStateName.Patrol);
            return;
        }
    }
    public override void ExitState()
    {
        
    }

    protected override void HandleMoveDir()
    {
        orc.moveDir=Vector3.Normalize((orc.GetPlayerPos().x*Vector3.right-orc.transform.position));
    }
}
