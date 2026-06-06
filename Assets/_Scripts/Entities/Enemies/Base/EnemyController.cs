using System;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EnemyController<T,V,S,F,H,J> : EntityController<T,V,S,H,J> where T: EntityMover where V: EntityVisual where S:EntityStats<H,J> where F:EnemyStateMachine where H:EnemyBaseStats where J:EnemyBaseMoveStats
{
    protected F enemyStateMachine;
    public PlayerController player{get; protected set;}   
    public Vector3 wardPosition;
    public float maxDistanceX;
    public float maxDistanceY;

    protected override void Awake()
    {
        base.Awake();
        GetEnemyComponents();
    }
    protected override void Start()
    {
        base.Start();
        moveDir=Vector2.zero;
        IsGrounded=mover.CheckGround();;
        stats.OnDie+=OnDie;
    }
    protected virtual void GetEnemyComponents()
    {
        player=FindFirstObjectByType<PlayerController>();
        wardPosition=transform.position;
    }
    protected virtual void ShowDebugRays()
    {
        Debug.DrawRay(wardPosition,Vector3.up*5,Color.green); //Posição de origem
        Debug.DrawRay(wardPosition+maxDistanceX*Vector3.right+maxDistanceY*Vector3.up,Vector3.down*maxDistanceY*2,Color.red); //limite
        Debug.DrawRay(wardPosition+maxDistanceX*Vector3.right+maxDistanceY*Vector3.up,Vector3.left*maxDistanceX*2,Color.red); //limite
        Debug.DrawRay(wardPosition-(maxDistanceX*Vector3.right+maxDistanceY*Vector3.up),Vector3.up*maxDistanceY*2,Color.red); //limite
        Debug.DrawRay(wardPosition-(maxDistanceX*Vector3.right+maxDistanceY*Vector3.up),Vector3.right*maxDistanceX*2,Color.red); //limite
        Debug.DrawLine(transform.position+stats.baseStats.rangeOfVision*Vector3.right,transform.position-stats.baseStats.rangeOfVision*Vector3.right,Color.blue);// raio de detecção
        Debug.DrawRay(wardPosition+Vector3.right*stats.baseStats.patrolRange,Vector3.up*maxDistanceX*2,Color.yellow);
        Debug.DrawRay(wardPosition-Vector3.right*stats.baseStats.patrolRange,Vector3.up*maxDistanceX*2,Color.yellow);
    }
    protected override void HandleExternalForces()
    {
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,stats.baseMoveStats.knockBackDecelaration*Time.fixedDeltaTime);
        if((transform.position.x>=wardPosition.x+maxDistanceX && externalForce.x>0) || (transform.position.x<=wardPosition.x-maxDistanceX && externalForce.x < 0))
        {
            externalForce=Vector2.zero;
        }
    }

    protected virtual void OnDie(object sender, EventArgs e)
    {
        Destroy(this);
        Destroy(mover);
        Destroy(stats);
        Destroy(visual);
    }

    protected virtual void Move()
    {
        mover.SetVelocity(frameVelocity+externalForce);
    }
    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    public void TotalDeath()
    {
        Destroy(gameObject);
    }
}
