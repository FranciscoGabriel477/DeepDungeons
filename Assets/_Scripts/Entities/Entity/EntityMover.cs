using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EntityMover : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected bool IsGrounded;
    protected bool IsHeadBump;
    [SerializeField] protected Vector2 colliderSize;
    [SerializeField] protected float offSet;
    [SerializeField] protected LayerMask groundLayerMask;
    public void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<Collider2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity=velocity;
    }

    public virtual bool CheckGround()
    {
       IsGrounded=Physics2D.Raycast(transform.position+offSet*Vector3.up,Vector2.down,colliderSize.y*0.5f+0.01f,groundLayerMask);
        if (IsGrounded)
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.down*(colliderSize.y*0.5f+0.01f),Color.green,Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.down*(colliderSize.y*0.5f+0.01f),Color.red,Time.deltaTime);
        }
        return IsGrounded;
    }
    public virtual bool CheckforHeadBump()
    {
       IsHeadBump=Physics2D.Raycast(transform.position+offSet*Vector3.up,Vector2.up,colliderSize.y*0.5f+0.01f,groundLayerMask);
        if (IsHeadBump)
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.up*(colliderSize.y*0.5f+0.01f),Color.green,Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.up*(colliderSize.y*0.5f+0.01f),Color.red,Time.deltaTime);
        }
        return IsHeadBump;
    }

    public void OnDestroy()
    {
        Destroy(rb);        
        Destroy(col);        
    }
}
