using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

[RequireComponent(typeof(Rigidbody2D))]
public class ThrowableAxe : MonoBehaviour
{
    public ThrowableAxeInfo tAxeInfo;
    public Vector2 direction=Vector2.right;
    public Collider2D tAxeCollider;
    public Rigidbody2D tAxeRb;
    private float traveledDistance=0;
    private float currentSpeed;
    private bool returnBack;
    private bool readyToTake;
    private void Awake()
    {
        tAxeCollider=GetComponent<Collider2D>();
        tAxeRb=GetComponent<Rigidbody2D>();
        currentSpeed=tAxeInfo.speed;
        returnBack=false;
        readyToTake=false;
    }

    private void FixedUpdate()
    {
        if(traveledDistance>=tAxeInfo.range)
        {
            readyToTake=true;
            if (math.abs(currentSpeed) <= tAxeInfo.speed && !returnBack)
            {
                currentSpeed+=tAxeInfo.decelaration*Time.fixedDeltaTime;  
            }
            else
            {
                currentSpeed=-tAxeInfo.speed;
                returnBack=true;
            }
        }
        else
        {
            traveledDistance+=tAxeInfo.speed*Time.fixedDeltaTime;
        }
        tAxeRb.linearVelocity=(Vector3)direction*currentSpeed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == tAxeInfo.groundLayer)
        {
                Destroy(gameObject);
                return;
        }
        if(collision.TryGetComponent<IHitable>(out IHitable targetHitted))
        {
            if (collision.gameObject.layer == tAxeInfo.enemyLayer)
            {
                targetHitted.GetHit(new HitInfo{damage=tAxeInfo.damage,knockBack=direction*tAxeInfo.knockBack,posOrigin=transform.position});
            }
            else if(collision.gameObject.layer == tAxeInfo.playerLayer && readyToTake)
            {
                PlayerHitBox playerHitBox= collision.gameObject.GetComponent<PlayerHitBox>();
                playerHitBox.GetAxe(tAxeInfo.cooldownReduce);
                Destroy(gameObject);
            }
            
        }
    }
    public void SetDir(Vector2 newDirection)
    {
        direction=newDirection;
    }
}
