using System;
using UnityEngine;

public class BeeMover : EnemyMover
{
    public override bool CheckGround()
    {
        IsGrounded=Physics2D.OverlapCapsule(transform.position,colliderSize*1.05f,CapsuleDirection2D.Vertical,0f,groundLayerMask);
        return IsGrounded;
    }
}
