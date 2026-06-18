using UnityEngine;

public class OrcWeapon : EntityWeapon<OrcWeaponInfo>
{
    protected override void Start()
    {
        base.Start();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(weaponInfo.attack1OffSet,weaponInfo.attack1Size);
    }
}
