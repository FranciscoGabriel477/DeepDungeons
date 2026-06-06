using System;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager: MonoBehaviour
{
    public static HUDManager instance{get; private set;}

    public Image lifeBar;
    public Image StaminaBar;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerLifeChange(object sender, PlayerStats.LifeInfo lifeInfo)
    {
        lifeBar.fillAmount=lifeInfo.currentLife/lifeInfo.currentMaxLife;
    }
    public void PlayerStaminaChange(object sender, PlayerStats.StaminaInfo staminaInfo)
    {
        StaminaBar.fillAmount=staminaInfo.currentStamina/staminaInfo.currentMaxStamina;
    }

}