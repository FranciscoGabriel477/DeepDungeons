using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(OrcMover))]

[RequireComponent(typeof(OrcStats))]
public class OrcController : EnemyController<OrcMover,OrcVisual,OrcStats,OrcStateMachine,OrcBaseStats,OrcBaseMoveStats>
{

    public OrcWeapon orcWeapon;
    protected override void Awake()
    {
        base.Awake();
        orcWeapon=GetComponentInChildren<OrcWeapon>();
    }
    protected override void Start()
    {
        base.Start();
        enemyStateMachine=new OrcStateMachine(this);
        enemyStateMachine.OnStateChanged+=visual.MainStateChanged;
    }

    protected override void Update()
    {
        base.Update();
        ShowDebugRays();
        enemyStateMachine.Action(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        IsGrounded=mover.CheckGround();
        enemyStateMachine.FixedAction(Time.fixedDeltaTime);
        HandleExternalForces();
        Move();
    }

    

}
