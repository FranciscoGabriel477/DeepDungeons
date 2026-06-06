using System;
using UnityEngine;
[CreateAssetMenu(menuName ="Bleed Base Stats")]

public class BleedData : EffectData
{
    public float intervalTick;
    public float damage;
    public override Effect<T,V,S,H,J> CreateInstance<T,V,S,H,J> (EntityController<T,V,S,H,J> entityController)
    {
        return new Bleed<T,V,S,H,J>(this,entityController);
    }
}
