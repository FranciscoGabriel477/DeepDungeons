using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats<T,Y> : EntityStats<T,Y> where T:EnemyBaseStats where Y:EnemyBaseMoveStats
{
    [SerializeField] protected Image lifeBar;
    [SerializeField] protected GameObject localHUD;
    [SerializeField] protected float currentStance;
    protected override void Start()
    {
        base.Start();
        currentStance=baseStats.stance;
        localHUD.gameObject.SetActive(false);
    }
    public void OnDestroy()
    {       
        Destroy(localHUD);        
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        localHUD.gameObject.SetActive(true);
        lifeBar.fillAmount=currentLife/currentMaxLife;
        currentStance-=damage;
    }

    public bool CheckForInstanceEnd()
    {
        if (currentStance <= 0)
        {
            currentStance=baseStats.stance;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color=Color.blue;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireSphere(Vector3.zero,baseStats.rangeOfVision);

        Gizmos.color=Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero,Vector3.up*baseStats.patrolRange*2 + Vector3.right*baseStats.patrolRange*2);
    }
}
