using UnityEngine;

[CreateAssetMenu(menuName ="ChargeWeaponStats")]

public class ChargeWeaponInfo : WeaponInfo
{
    public Vector2 fastAttackSize;
    public Vector2 fastAttackOffSet;
    public LayerMask enemyLayer;
}
