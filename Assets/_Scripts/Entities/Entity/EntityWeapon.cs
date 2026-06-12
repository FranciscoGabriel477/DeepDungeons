using System.Collections.Generic;
using UnityEngine;

public class EntityWeapon : MonoBehaviour
{
    public List<Collider2D> weaponColliders;
    public WeaponInfo weaponInfo;
    public ContactFilter2D contactFilter;
    protected virtual void Start()
    {
        contactFilter.useLayerMask=true;
        for(int i = 0; i < weaponColliders.Count; i++)
        {
            DisableWeapon(i);
        }
    }
    public virtual void Attack(float dir)
    {
        RaycastHit2D[] enemiesHitted=new RaycastHit2D[5];
        weaponColliders[0].Cast(Vector3.zero,contactFilter,enemiesHitted);
        if (enemiesHitted[0])
        {
            Vector2 KnockBackDir=transform.position.x-enemiesHitted[0].transform.position.x<0?Vector2.right:Vector2.left;
            enemiesHitted[0].collider.gameObject.GetComponent<IHitable>().GetHit(new HitInfo{damage=weaponInfo.damage, knockBack=KnockBackDir*weaponInfo.knockBackImpulse,posOrigin=transform.position});
        }
    }
    public virtual void EnableWeapon(int i)
    {
        weaponColliders[i].enabled=true;
    }
    public virtual void DisableWeapon(int i)
    {
        weaponColliders[i].enabled=false;
    }
    protected virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
}
