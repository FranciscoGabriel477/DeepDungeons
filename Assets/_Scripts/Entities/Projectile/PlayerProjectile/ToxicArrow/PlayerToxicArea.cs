using UnityEngine;

public class PlayerToxicArea : MonoBehaviour
{
    [SerializeField] private ToxicAreaInfo toxicAreaInfo;
    private float currentTime;
    private float currentTimeTick;
    private void Start()
    {
        currentTime=toxicAreaInfo.totalDuration;
        currentTimeTick=toxicAreaInfo.tickTime;
    }

    private void Update()
    {
        currentTime-=Time.deltaTime;
        currentTimeTick-=Time.deltaTime;
        if (currentTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IHitable>(out IHitable targetHitted))
        {
            if (currentTimeTick <= 0)
            {
                targetHitted.GetHit(new HitInfo{damage=toxicAreaInfo.damage, knockBack=Vector2.zero, posOrigin=transform.position,stanceDamage=0f});
                currentTimeTick=toxicAreaInfo.tickTime;
                
            }
        }
    }
}
