using UnityEngine;

public class EntityWeapon : MonoBehaviour
{
    public Collider2D weaponCollider;
    public WeaponInfo weaponInfo;
    public ContactFilter2D contactFilter;
    protected virtual void Start()
    {
        contactFilter.useLayerMask=true;
    }
    public virtual void Attack(float dir)
    {
        RaycastHit2D[] enemiesHitted=new RaycastHit2D[5];
        weaponCollider.Cast(Vector3.zero,contactFilter,enemiesHitted);
        if (enemiesHitted[0])
        {
            Vector2 KnockBackDir=transform.position.x-enemiesHitted[0].transform.position.x<0?Vector2.right:Vector2.left;
            enemiesHitted[0].collider.gameObject.GetComponent<IHitable>().GetHit(new HitInfo{damage=weaponInfo.damage, knockBack=KnockBackDir*weaponInfo.knockBackImpulse,posOrigin=transform.position});
        }
    }

    protected virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
}
