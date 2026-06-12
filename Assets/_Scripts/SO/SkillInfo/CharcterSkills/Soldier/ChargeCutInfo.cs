using UnityEngine;

[CreateAssetMenu(menuName ="ChargeCutInfo Stats")]

public class ChargeCutInfo : SkillInfo
{
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public float finishCutTime;
    public float timeToTotalDamge;
    public float baseDamage;
    public float chargeDamage;
    public float baseKnockBack;
    public float chargeKnockBack;
}
