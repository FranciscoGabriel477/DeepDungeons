using Unity.VisualScripting;
using UnityEngine;

public class PatrolOrcState : OrcState
{
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    public PatrolOrcState(OrcStateMachine parent,OrcController orc) : base(parent, OrcStateName.Patrol, orc){}

    public override void EntryState()
    {
        initialPosition=orc.transform.position;
        targetPosition=orc.wardPosition+Random.Range(-orc.baseStats.patrolRange,orc.baseStats.patrolRange)*Vector3.right;
        //Debug.Log(targetPosition+" "+orc.wardPosition);
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        if (Mathf.InverseLerp(initialPosition.x,targetPosition.x,orc.transform.position.x)>0.97f)
        {
            parent.SwitchState(OrcStateName.Ward);
            return;
        }
        if ((orc.GetPlayerPos()-orc.transform.position).magnitude<orc.baseStats.rangeOfVision)
        {
            parent.SwitchState(OrcStateName.Chase);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
    }
    protected override void HandleMoveDir()
    {
        orc.moveDir=Vector3.Normalize(targetPosition-orc.transform.position);
    }
}
