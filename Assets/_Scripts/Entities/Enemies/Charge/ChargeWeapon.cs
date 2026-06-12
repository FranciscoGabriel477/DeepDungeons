using System;
using UnityEngine;

public class ChargeWeapon : EntityWeapon
{
    public event EventHandler OnEnemyHitted;
    protected override void Start()
    {
        base.Start();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable enemyHitted;
        collision.gameObject.TryGetComponent<IHitable>(out enemyHitted);
        if (enemyHitted != null)
        {
            Vector2 KnockBackDir=transform.position.x-collision.transform.position.x<0?Vector2.right:Vector2.left;
            enemyHitted.GetHit(new HitInfo{damage=weaponInfo.damage, knockBack=KnockBackDir*weaponInfo.knockBackImpulse,posOrigin=transform.position});
            OnEnemyHitted?.Invoke(this,EventArgs.Empty);
        }
    }
    
}
