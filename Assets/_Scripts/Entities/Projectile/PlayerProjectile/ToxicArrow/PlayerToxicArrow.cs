using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerToxicArrow : MonoBehaviour
{
    public ToxicArrowProjectileInfo projectileInfo;
    public Vector2 direction=Vector2.right;
    public Collider2D arrowCollider;
    public int groundLayer;
    private float traveledDistance=0;
    private int currentPierce;
    private void Awake()
    {
        arrowCollider=GetComponent<Collider2D>();
        currentPierce=projectileInfo.pierce;
    }
    void Start()
    {
        
    }
    void Update()
    {
        transform.position+=(Vector3)direction*projectileInfo.speed*Time.deltaTime;
        traveledDistance+=projectileInfo.speed*Time.deltaTime;
        if (traveledDistance > projectileInfo.range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == groundLayer)
        {
            Instantiate(projectileInfo.toxicArea,transform.position,Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        if(collision.TryGetComponent<IHitable>(out IHitable targetHitted))
        {
            targetHitted.GetHit(new HitInfo{damage=projectileInfo.damage,knockBack=direction*projectileInfo.knockBack,posOrigin=transform.position});
            Instantiate(projectileInfo.toxicArea,transform.position,Quaternion.identity);
            if (--currentPierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
