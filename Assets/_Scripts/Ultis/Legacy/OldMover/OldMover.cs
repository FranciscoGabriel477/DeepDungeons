using UnityEngine;

public class OldMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private bool IsGrounded;
    [SerializeField] private float colliderHeight;
    [SerializeField] private float colliderThickness;
    [SerializeField] private float colliderStepRatio;
    [SerializeField] private LayerMask groundLayerMask;
    private Vector2 groundAdjustmentVelocity;
    public void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<CapsuleCollider2D>();
        //RecalibrateCollider();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity=velocity+groundAdjustmentVelocity;
    }

    public bool CheckGround()
    {
       IsGrounded=Physics2D.Raycast(transform.position,Vector2.down,colliderHeight*0.5f+0.01f,groundLayerMask);
        if (IsGrounded)
        {
        Debug.DrawRay(transform.position,Vector2.down*(colliderHeight*0.5f+0.01f),Color.green,Time.deltaTime);
        //groundAdjustmentVelocity=Vector2.up*((colliderHeight*0.5f-hitInfo.distance)/Time.fixedDeltaTime);
        }
        else
        {
        Debug.DrawRay(transform.position,Vector2.down*(colliderHeight*0.5f+0.01f),Color.red,Time.deltaTime);
        //groundAdjustmentVelocity=Vector2.zero;
        }
        return IsGrounded;
    }

    // private void RecalibrateCollider()
    // {
    //     col.size=new Vector2(colliderThickness/2,colliderHeight*(1-colliderStepRatio));
    //     col.offset=new Vector2(0,colliderHeight*colliderStepRatio*0.5f);
    // }
}
