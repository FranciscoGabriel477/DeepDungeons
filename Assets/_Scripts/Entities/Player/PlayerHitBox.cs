using System;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour,IHitable
{
    private Collider2D playerHitBox;
    public EventHandler<HitInfo> OnHit;
    public EventHandler<CooldownRecovery> OnAxeTaked;

    public class CooldownRecovery
    {
        public float colldownRecovery;
    }
    private void Awake()
    {
        playerHitBox=GetComponent<Collider2D>();
    }

    public void GetHit(HitInfo hitInfo)
    {
        OnHit?.Invoke(this,hitInfo);
    }
    public void HandleHitBox(bool isinvencible)
    {
        if (playerHitBox == null)
        {
            return;
        }
        if(playerHitBox.enabled)
        {
            if(isinvencible){
                playerHitBox.enabled=false;
            }
        }
        else
        {
            if(!isinvencible){
                playerHitBox.enabled=true;
            }
        }
    }

    public void DestroyPlayerHitBox()
    {
        playerHitBox.enabled=false;
        playerHitBox=null;
    }

    public void GetAxe(float cooldownReduce)
    {
        OnAxeTaked?.Invoke(this,new CooldownRecovery{colldownRecovery=cooldownReduce});
    }
}
