using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EntityMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private bool IsGrounded;
    [SerializeField] private float colliderHeight;
    [SerializeField] private LayerMask groundLayerMask;
    public void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<CapsuleCollider2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity=velocity;
    }

    public bool CheckGround()
    {
       IsGrounded=Physics2D.Raycast(transform.position,Vector2.down,colliderHeight*0.5f+0.01f,groundLayerMask);
        if (IsGrounded)
        {
            Debug.DrawRay(transform.position,Vector2.down*(colliderHeight*0.5f+0.01f),Color.green,Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(transform.position,Vector2.down*(colliderHeight*0.5f+0.01f),Color.red,Time.deltaTime);
        }
        return IsGrounded;
    }

    
}
