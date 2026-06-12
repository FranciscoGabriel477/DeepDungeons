using System.Collections.Generic;
using UnityEngine;

public interface IHitable 
{
    public abstract void GetHit(HitInfo hitInfo);
}

public class HitInfo
{
    public Vector2 knockBack;
    public Vector2 posOrigin;
    public List<EffectData> effectsOnHit;
    public float damage;
    public float stanceDamage;
}
