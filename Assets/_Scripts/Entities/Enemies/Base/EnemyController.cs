using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EnemyController<T,V,S,F,H,J> : EntityController<T,V,S,H,J> where T: EnemyMover where V: EnemyVisual where S:EnemyStats<H,J> where F:EnemyStateMachine where H:EnemyBaseStats where J:EnemyBaseMoveStats
{
    protected F enemyStateMachine;
    public PlayerController player{get; protected set;}   
    public Vector3 wardPosition;
    public float maxDistanceX;
    public float maxDistanceY;
    public List<float> attacksCooldowns;
    public bool allAttacksOnCooldown=false;
    protected override void Awake()
    {
        base.Awake();
        GetEnemyComponents();
    }
    protected override void Start()
    {
        base.Start();
        SetupAttackListCooldown();
        stats.OnDie+=OnDie;
    }

    protected virtual void Update()
    {
        UpdateAttackListCooldown(); 
    }
    protected virtual void SetupAttackListCooldown()
    {
        attacksCooldowns= new List<float>();
        for(int i = 0; i < stats.baseStats.attackDatas.Count; i++)
        {
            attacksCooldowns.Add(0f);
        }
    }

    protected virtual void UpdateAttackListCooldown()
    {
        allAttacksOnCooldown=true;
        for(int i = 0; i < stats.baseStats.attackDatas.Count; i++)
        {
            attacksCooldowns[i]-=Time.deltaTime;
            attacksCooldowns[i]=math.max(0f,attacksCooldowns[i]);
            if (attacksCooldowns[i] == 0f)
            {
                allAttacksOnCooldown=false;
            }
        }
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero,Vector3.right*maxDistanceX*2+Vector3.up*maxDistanceY*2);
    }
    public override void HandleExternalForces()
    {
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,stats.baseMoveStats.knockBackDecelaration*Time.fixedDeltaTime);
        if((transform.position.x>=wardPosition.x+maxDistanceX && externalForce.x>0) || (transform.position.x<=wardPosition.x-maxDistanceX && externalForce.x < 0))
        {
            externalForce=Vector2.zero;
        }
    }

    protected virtual void OnDie(object sender, EventArgs e)
    {
        player.stats.GetExperience(stats.baseStats.experienceDroped);
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

    public void TotalDeath(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public override void GetHit(HitInfo hitInfo)
    {
        base.GetHit(hitInfo);
        stats.TakeDamage(hitInfo.damage);
        if (stats.CheckForInstanceEnd())
        {
            enemyStateMachine.SwitchState("Hurt");
        }
        visual.StartFlahshing(0.05f,0.2f);
        
    }
}
