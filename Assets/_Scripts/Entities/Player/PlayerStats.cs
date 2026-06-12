using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class PlayerStats : EntityStats<PlayerBaseStats,PlayerBaseMoveStats>
{
    public float currentStamina{get; protected set;}
    public float currentMaxStamina{get; protected set;}
    public float experienceToUp;
    public float currentExperience;
    public event EventHandler<LifeInfo> OnLifeChange;
    public event EventHandler<StaminaInfo> OnStaminaChange;
    public event EventHandler<ExperienceInfo> OnExperienceChange;
    public event EventHandler OnDeath;

    public class LifeInfo
    {
        public float currentLife;
        public float currentMaxLife;
    }

    public class StaminaInfo
    {
        public float currentStamina;
        public float currentMaxStamina;
    }
    public class ExperienceInfo
    {
        public float currentExperience;
        public float experienceToUp;
    }

    protected override void Start()
    {
        base.Start();
        currentMaxStamina=baseStats.maxStamina;   
        currentStamina=baseStats.maxStamina;
        currentExperience=0;  
        experienceToUp=baseStats.experienceToUp;
        OnLifeChange+=HUDManager.instance.PlayerLifeChange;
        OnStaminaChange+=HUDManager.instance.PlayerStaminaChange;
       // OnExperienceChange+=HUDManager.instance.PlayerExperienceChange;
    }

    protected void Update()
    {
        HandleStaminaRecovery();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnLifeChange?.Invoke(this,new LifeInfo{currentLife=currentLife, currentMaxLife=currentMaxLife});
        if (currentLife <= 0)
        {
            OnDeath?.Invoke(this,EventArgs.Empty);
        }
    }

    public void ConsumeStamina(float staminaUsed)
    {
        currentStamina-=staminaUsed;
        currentStamina=math.max(0f,currentStamina);
        OnStaminaChange?.Invoke(this,new StaminaInfo{currentStamina=currentStamina, currentMaxStamina=currentMaxStamina});
    }

    private void HandleStaminaRecovery()
    {
        currentStamina+=baseStats.staminaRecoveryVelocity*Time.deltaTime;
        currentStamina=math.min(currentStamina,currentMaxStamina);
        OnStaminaChange?.Invoke(this,new StaminaInfo{currentStamina=currentStamina, currentMaxStamina=currentMaxStamina});
    }

    public void GetExperience(float experience)
    {
        currentExperience+=experience;
        OnExperienceChange?.Invoke(this,new ExperienceInfo{currentExperience=currentExperience, experienceToUp=experienceToUp});
        if (experience == 0)
        {
            return;
        }
        if (currentExperience >= experienceToUp)
        {
            Debug.Log("Upou");
            //HUDManager.instance.PlayerLevelUp();
            float excess=currentExperience-experienceToUp;
            currentExperience=0;
            experienceToUp+=baseStats.experienceIncrementToNextLevel;
            GetExperience(excess);
        }
    }

    public void TakeTrapDamage()
    {
        currentLife=math.max(1f,currentLife-base.baseStats.trapDammage);
        OnLifeChange?.Invoke(this,new LifeInfo{currentLife=currentLife, currentMaxLife=currentMaxLife});
    }
    
}