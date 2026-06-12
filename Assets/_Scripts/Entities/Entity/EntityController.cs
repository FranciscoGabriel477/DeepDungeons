using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EntityController<T,V,S,H,J> : MonoBehaviour,IHitable  where T: EntityMover where V: EntityVisual where S:EntityStats<H,J> where H:EntityBaseStats where J:EntityBaseMoveStats
{
    [HideInInspector] public T mover;
    [HideInInspector] public V visual;
    [HideInInspector] public S stats;
    public Vector2 moveDir;
    public Vector2 frameVelocity;
    public Vector2 externalForce;
    public bool isFacingRight;
    public bool IsGrounded{get; protected set;}
    public bool IsHeadBump{get; protected set;}
    public List<Effect<T,V,S,H,J>> effects{get; protected set;}
    public List<Effect<T,V,S,H,J>> effectsToRemove{get; protected set;}


    protected virtual void Awake()
    {
        GetComponents();
    }
    protected virtual void Start()
    {
        effects = new List<Effect<T,V,S,H,J>>();
        effectsToRemove = new List<Effect<T,V,S,H,J>>();
        isFacingRight=true;
        IsGrounded=mover.CheckGround();
        IsHeadBump=mover.CheckforHeadBump();
    }

    private void GetComponents()
    {
        mover=GetComponent<T>();
        visual=GetComponentInChildren<V>();
        stats=GetComponent<S>();
    }
    public virtual void SetHorizontalFrameVelocity(float newVelocityDirX)
    {
        frameVelocity.x=newVelocityDirX;
    }

    public virtual void SetVerticalFrameVelocity(float newVelocityY)
    {
        frameVelocity.y=newVelocityY;
    }

    public virtual void GetHit(HitInfo hitInfo)
    {
        externalForce+=hitInfo.knockBack;
        externalForce=Vector2.ClampMagnitude(externalForce,stats.baseMoveStats.maxKnockBack);
        if(hitInfo.effectsOnHit != null)
        {
            foreach(EffectData effectData in hitInfo.effectsOnHit)
            {
                AddEffect(effectData);
            }
        }
        
    }
    protected virtual void HandleExternalForces(){}

    protected virtual void HandleEffects()
    {
        foreach(Effect<T,V,S,H,J> e in effects)
        {
            e.ProcessEffect(Time.deltaTime);  
        }

        foreach(Effect<T,V,S,H,J> e in effectsToRemove)
        {
            e.ExitEffect(); 
            effects.Remove(e);
        }
        effectsToRemove.Clear();
    }

    public virtual void AddEffect(EffectData effectData)
    {
        Effect<T,V,S,H,J> effectRepeated = effects.Find(x => x.effectData.effectName == effectData.effectName);        
        if (effectRepeated!=null)
        {
            effectRepeated.AddStack();
            Debug.Log("Mesmo efeito");
        }
        else
        {
            Effect<T,V,S,H,J> effect = effectData.CreateInstance<T, V, S, H, J>(this);
            effects.Add(effect);
            effect.EntryEffect();
        }
    }

    public virtual void RemoveEffect(Effect<T,V,S,H,J> effect)
    {
        effectsToRemove.Add(effect);
    }

    
}

