using UnityEngine;

[CreateAssetMenu(menuName ="SoldierWeaponStats")]

public class SoldierWeaponInfo : WeaponInfo
{
    public float attackTimeOnMeele1;
    public float staminaCostOnMeele1;
    public Vector2 attackMeele1Size;
    public Vector2 attackMeele1OffSet;
    public Vector2 airAttackMeele1Size;
    public Vector2 airAttackMeele1OffSet;
    public LayerMask enemyLayer;
}
