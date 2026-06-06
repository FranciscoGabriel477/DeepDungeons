using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(ChargeMover))]

[RequireComponent(typeof(ChargeStats))]
public class ChargeController : EnemyController<ChargeMover,ChargeVisual,ChargeStats,ChargeStateMachine,ChargeBaseStats,ChargeBaseMoveStats>
{
    public ChargeWeapon chargeWeapon;

    protected override void Awake()
    {
        base.Awake();
        chargeWeapon=GetComponentInChildren<ChargeWeapon>();
    }
    protected override void Start()
    {
        base.Start();
        enemyStateMachine=new ChargeStateMachine(this);
        enemyStateMachine.OnStateChanged+=visual.MainStateChanged;
    }

    private void Update()
    {
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

    public override void GetHit(HitInfo hitInfo)
    {
        base.GetHit(hitInfo);
        enemyStateMachine.SwitchState(ChargeStateName.Hurt);
        stats.TakeDamage(hitInfo.damage);
    }
    protected override void OnDie(object sender, EventArgs e)
    {
        base.OnDie(sender,e);
        Destroy(chargeWeapon);
    }
}
