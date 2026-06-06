using System;
using UnityEngine;

public class BeeController : EnemyController<BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public BeeWeapon beeWeapon;
    protected override void Awake()
    {
        base.Awake();
        beeWeapon=GetComponentInChildren<BeeWeapon>();
        
    }
    protected override void Start()
    {
        base.Start();
        enemyStateMachine=new BeeStateMachine(this);
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
        enemyStateMachine.SwitchState(BeeStateName.Hurt);
        stats.TakeDamage(hitInfo.damage);
    }

    protected override void ShowDebugRays()
    {
        Debug.DrawRay(wardPosition,Vector3.up*5,Color.green); //Posição de origem
        Debug.DrawRay(wardPosition+maxDistanceX*Vector3.right+maxDistanceY*Vector3.up,Vector3.down*maxDistanceY*2,Color.red); //limite
        Debug.DrawRay(wardPosition+maxDistanceX*Vector3.right+maxDistanceY*Vector3.up,Vector3.left*maxDistanceX*2,Color.red); //limite
        Debug.DrawRay(wardPosition-(maxDistanceX*Vector3.right+maxDistanceY*Vector3.up),Vector3.up*maxDistanceY*2,Color.red); //limite
        Debug.DrawRay(wardPosition-(maxDistanceX*Vector3.right+maxDistanceY*Vector3.up),Vector3.right*maxDistanceX*2,Color.red); //limite
        Debug.DrawLine(transform.position+stats.baseStats.rangeOfVision*Vector3.right,transform.position-stats.baseStats.rangeOfVision*Vector3.right,Color.blue);// raio de detecção
        Debug.DrawRay(wardPosition+stats.baseStats.patrolRange*Vector3.right+stats.baseStats.patrolRangeY*Vector3.up,Vector3.down*stats.baseStats.patrolRangeY*2,Color.yellow); 
        Debug.DrawRay(wardPosition+stats.baseStats.patrolRange*Vector3.right+stats.baseStats.patrolRangeY*Vector3.up,Vector3.left*stats.baseStats.patrolRange*2,Color.yellow); 
        Debug.DrawRay(wardPosition-(stats.baseStats.patrolRange*Vector3.right+stats.baseStats.patrolRangeY*Vector3.up),Vector3.up*stats.baseStats.patrolRangeY*2,Color.yellow); 
        Debug.DrawRay(wardPosition-(stats.baseStats.patrolRange*Vector3.right+stats.baseStats.patrolRangeY*Vector3.up),Vector3.right*stats.baseStats.patrolRange*2,Color.yellow); 
    }

    protected override void OnDie(object sender, EventArgs e)
    {
        base.OnDie(sender,e);
        Destroy(beeWeapon);
    }
}
