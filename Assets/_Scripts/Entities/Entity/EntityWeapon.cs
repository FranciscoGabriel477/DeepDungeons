using UnityEngine;

public class EntityWeapon : MonoBehaviour
{
    public Collider2D weaponCollider;
    public WeaponInfo weaponInfo;
    public ContactFilter2D contactFilter;
    public LayerMask targetLayer;
    private void Start()
    {
        contactFilter.SetLayerMask(targetLayer);
        contactFilter.useLayerMask=true;
    }
    public virtual void Attack(float dir)
    {
        RaycastHit2D[] enemiesHitted=new RaycastHit2D[5];
        weaponCollider.Cast(Vector3.zero,contactFilter,enemiesHitted);
        if (enemiesHitted[0])
        {
            Vector2 KnockBackDir=transform.position.x-enemiesHitted[0].transform.position.x<0?Vector2.right:Vector2.left;
            enemiesHitted[0].collider.gameObject.GetComponent<EntityController>().GetHit(weaponInfo.damage,KnockBackDir*weaponInfo.knockBackImpulse);
        }
    }
}
