using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats<T,Y> : EntityStats<T,Y> where T:EnemyBaseStats where Y:EnemyBaseMoveStats
{
    [SerializeField] protected Image lifeBar;
    [SerializeField] protected GameObject localHUD;
    protected override void Start()
    {
        base.Start();
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
    }
}
