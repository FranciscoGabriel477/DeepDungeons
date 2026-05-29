using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerArrow : MonoBehaviour
{
    public ProjectileInfo projectileInfo;
    public Vector2 direction=Vector2.right;
    public Collider2D arrowCollider;
    private float traveledDistance=0;
    private void Awake()
    {
        arrowCollider=GetComponent<Collider2D>();
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
        if(collision.TryGetComponent<IHitable>(out IHitable targetHitted))
        {
            targetHitted.GetHit(new HitInfo{damage=projectileInfo.damage,knockBack=direction*projectileInfo.knockBack,posOrigin=transform.position});
            if (--projectileInfo.pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
