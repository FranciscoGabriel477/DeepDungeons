using UnityEngine;

public class EntityController : MonoBehaviour, IHitable
{
    public Vector2 moveDir;
    public Vector2 frameVelocity;
    public Vector2 externalForce;
    public bool isFacingRight;
    public bool IsGrounded{get; protected set;}

    public virtual void SetHorizontalFrameVelocity(float newVelocityDirX)
    {
        frameVelocity.x=newVelocityDirX;
    }

    public virtual void SetVerticalFrameVelocity(float newVelocityY)
    {
        frameVelocity.y=newVelocityY;
        
    }

    public virtual void GetHit(HitInfo hitInfo)
    {
        externalForce=hitInfo.knockBack;
    }
}

