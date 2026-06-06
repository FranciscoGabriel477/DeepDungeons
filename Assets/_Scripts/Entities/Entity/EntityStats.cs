using System;
using Unity.Mathematics;
using UnityEngine;

public class EntityStats<T,Y> : MonoBehaviour where T: EntityBaseStats where Y: EntityBaseMoveStats
{
    public event EventHandler OnDie;
    public T baseStats;
    public Y baseMoveStats;
    public float currentMaxLife{get; protected set;}
    public float currentLife{get; protected set;}
    public float currentSpeed{get; protected set;}   
    protected virtual void Start()
    {
        currentLife=baseStats.maxLife;
        currentMaxLife=baseStats.maxLife;
        currentSpeed=baseMoveStats.moveHorizontalSpeed;
    } 
    public virtual void TakeDamage(float damage)
    {
        currentLife-=damage;
        currentLife=math.max(currentLife,0);
        if (currentLife <= 0)
        {
            OnDie?.Invoke(this,EventArgs.Empty);
        }
    }
}