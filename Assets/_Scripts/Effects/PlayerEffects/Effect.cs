using System;
using UnityEngine;

public abstract class Effect<T,V,S,H,J> where T: EntityMover where V: EntityVisual where S:EntityStats<H,J> where H:EntityBaseStats where J:EntityBaseMoveStats
{
    protected float actualDuration;
    protected int stacks;
    public EffectData effectData{get; protected set;}
    protected EntityController<T,V,S,H,J> entityController;
    public Effect(EffectData effectData, EntityController<T,V,S,H,J> entityController)
    {
        this.entityController=entityController;
        this.effectData=effectData;
        actualDuration=effectData.totalDuration;
        stacks=1;
    }
    public abstract void EntryEffect();
    public virtual void ProcessEffect(float deltaTime)
    {
        actualDuration-=deltaTime;
        if (actualDuration <= 0)
        {
            entityController.RemoveEffect(this);
        }
    }
    public abstract void ExitEffect();
    public virtual void AddStack()
    {
        stacks++;
        actualDuration=effectData.totalDuration;
    }
}
