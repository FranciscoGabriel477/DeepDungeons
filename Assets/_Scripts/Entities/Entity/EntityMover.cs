using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EntityMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private bool IsGrounded;
    private bool IsHeadBump;
    [SerializeField] private float colliderHeight;
    [SerializeField] private float offSet;
    [SerializeField] private LayerMask groundLayerMask;
    public void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<Collider2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity=velocity;
    }

    public bool CheckGround()
    {
       IsGrounded=Physics2D.Raycast(transform.position+offSet*Vector3.up,Vector2.down,colliderHeight*0.5f+0.01f,groundLayerMask);
        if (IsGrounded)
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.down*(colliderHeight*0.5f+0.01f),Color.green,Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.down*(colliderHeight*0.5f+0.01f),Color.red,Time.deltaTime);
        }
        return IsGrounded;
    }
    public bool CheckforHeadBump()
    {
       IsHeadBump=Physics2D.Raycast(transform.position+offSet*Vector3.up,Vector2.up,colliderHeight*0.5f+0.01f,groundLayerMask);
        if (IsHeadBump)
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.up*(colliderHeight*0.5f+0.01f),Color.green,Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(transform.position+offSet*Vector3.up,Vector2.up*(colliderHeight*0.5f+0.01f),Color.red,Time.deltaTime);
        }
        return IsHeadBump;
    }

    public void OnDestroy()
    {
        Destroy(rb);        
        Destroy(col);        
    }
}
