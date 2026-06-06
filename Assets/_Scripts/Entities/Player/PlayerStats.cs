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
    public event EventHandler<LifeInfo> OnLifeChange;
    public event EventHandler<StaminaInfo> OnStaminaChange;

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

    protected override void Start()
    {
        base.Start();
        currentMaxStamina=baseStats.maxStamina;   
        currentStamina=baseStats.maxStamina;   
        OnLifeChange+=HUDManager.instance.PlayerLifeChange;
        OnStaminaChange+=HUDManager.instance.PlayerStaminaChange;
    }

    protected void Update()
    {
        HandleStaminaRecovery();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnLifeChange?.Invoke(this,new LifeInfo{currentLife=currentLife, currentMaxLife=currentMaxLife});
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
}