using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Vector3 boxSize;
    public Color debugColor;
    public Vector3 centerOffSet;
    public LayerMask enemyLayer;
    public WeaponInfo weaponInfo;

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        
        Vector3 globalCenter = transform.position + centerOffSet;

        Gizmos.DrawWireCube(globalCenter, boxSize);
    }

    public void Attack(float dir)
    {
        Vector3 auxCenterOffSet=centerOffSet;
        if (dir == 180)
        {
            auxCenterOffSet.x=-auxCenterOffSet.x;
        }
        RaycastHit2D enemyHitted=Physics2D.BoxCast(transform.position + auxCenterOffSet, boxSize, 0f, Vector2.right,0f,enemyLayer);
        if (enemyHitted.collider)
        {
            Vector2 KnockBackDir=transform.position.x-enemyHitted.transform.position.x<0?Vector2.right:Vector2.left;
            enemyHitted.collider.gameObject.GetComponent<EntityController>().GetHit(weaponInfo.damage,KnockBackDir*weaponInfo.knockBackImpulse);
        }
    }
}
