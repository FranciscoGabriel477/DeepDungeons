using System;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour,IHitable
{
    private Collider2D playerHitBox;
    public EventHandler<HitInfo> OnHit;
    private void Awake()
    {
        playerHitBox=GetComponent<Collider2D>();
    }

    public void GetHit(HitInfo hitInfo)
    {
        Debug.Log("Ai");
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
}
