using System;
using UnityEngine;

public abstract class EffectData : ScriptableObject
{
    public Sprite effectIcon;
    public float totalDuration;
    public string effectName;
    public abstract Effect<T,V,S,H,J> CreateInstance<T,V,S,H,J> (EntityController<T,V,S,H,J> entityController)where T: EntityMover where V: EntityVisual where S:EntityStats<H,J> where H:EntityBaseStats where J:EntityBaseMoveStats;
}
