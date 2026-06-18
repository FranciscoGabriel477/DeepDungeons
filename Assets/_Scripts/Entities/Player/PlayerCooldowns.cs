using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCooldowns : MonoBehaviour
{
    public float currentTimerDashCoolDown{get; private set;}
    public float currentTimerCoyoteTime{get; private set;}
    public float currentTimerJumpBuffer{get; private set;}
    public float currentTimerBlockCoolDown{get; private set;}
    public float currentInvencibilityTime{get; private set;}
    public float currentSkill1Cooldown;
    public float currentSkill2Cooldown;
    public float totalSkill1Cooldown;
    public float totalSkill2Cooldown;
    public EventHandler<SkillCooldown> OnSkill1CooldownChanged;
    public EventHandler<SkillCooldown> OnSkill2CooldownChanged;
    public class SkillCooldown
    {
        public float total;
        public float current;
    }
    private void Start()
    {
        OnSkill1CooldownChanged+=HUDManager.instance.HandleSkill1Cooldown;
        OnSkill2CooldownChanged+=HUDManager.instance.HandleSkill2Cooldown;
    }
    private void Update()
    {
        CountTimers();
    }
    public void CountTimers()
    {
        currentTimerJumpBuffer=Math.Max(0,currentTimerJumpBuffer-Time.deltaTime);
        currentTimerCoyoteTime=Math.Max(0,currentTimerCoyoteTime-Time.deltaTime);
        currentTimerDashCoolDown=Math.Max(0,currentTimerDashCoolDown-Time.deltaTime);
        currentTimerBlockCoolDown=Math.Max(0,currentTimerBlockCoolDown-Time.deltaTime);
        currentInvencibilityTime=Math.Max(0,currentInvencibilityTime-Time.deltaTime);

        currentSkill1Cooldown=Math.Max(0,currentSkill1Cooldown-Time.deltaTime);
        currentSkill2Cooldown=Math.Max(0,currentSkill2Cooldown-Time.deltaTime);
        if (totalSkill1Cooldown != 0)
        {
            OnSkill1CooldownChanged?.Invoke(this, new SkillCooldown{total= totalSkill1Cooldown, current= currentSkill1Cooldown});
        }
        if(totalSkill2Cooldown != 0)
        {
            OnSkill2CooldownChanged?.Invoke(this, new SkillCooldown{total= totalSkill2Cooldown, current=currentSkill2Cooldown});
        }
  
    }
    public bool InInvencibilityTime()=> currentInvencibilityTime>0;
    public bool Skill1InCooldown()=> currentSkill1Cooldown>0;
    public bool Skill2InCooldown()=> currentSkill2Cooldown>0;
    public bool DashInCooldown()=> currentTimerDashCoolDown>0;
    public bool BlockInCooldown()=> currentTimerBlockCoolDown>0;
    public bool InJumpBuffer()=> currentTimerJumpBuffer>0;
    public bool InCoyoteTime()=> currentTimerCoyoteTime>0;
    
    public void JumpBufferStart(object sender, PlayerController.CooldownCount buffer)
    {
        currentTimerJumpBuffer=buffer.cooldown;
    }
    public void CoyoteTimeStart(object sender, PlayerController.CooldownCount timer)
    {
        currentTimerCoyoteTime=timer.cooldown;
    }
    public void DashCooldownStart(object sender, PlayerController.CooldownCount cooldown)
    {
        currentTimerDashCoolDown=cooldown.cooldown;
    }
    public void BlockCooldownStart(object sender, PlayerController.CooldownCount cooldown)
    {
        currentTimerBlockCoolDown=cooldown.cooldown;
    }
    public void Skill1CooldownStart(object sender, PlayerController.CooldownCount cooldown)
    {
        totalSkill1Cooldown=cooldown.cooldown;
        currentSkill1Cooldown=cooldown.cooldown;
    }
    public void Skill2CooldownStart(object sender, PlayerController.CooldownCount cooldown)
    {
        totalSkill2Cooldown=cooldown.cooldown;
        currentSkill2Cooldown=cooldown.cooldown;
    }
    public void Skill1CooldownReduce(object sender, PlayerController.CooldownCount cooldown)
    {
        currentSkill1Cooldown-=cooldown.cooldown;
        if (totalSkill1Cooldown != 0)
        {
            OnSkill1CooldownChanged?.Invoke(this, new SkillCooldown{total= totalSkill1Cooldown, current= currentSkill1Cooldown});
        }
    }
    public void Skill2CooldownReduce(object sender, PlayerController.CooldownCount cooldown)
    {
        currentSkill2Cooldown-=cooldown.cooldown;
        if (totalSkill2Cooldown != 0)
        {
            OnSkill2CooldownChanged?.Invoke(this, new SkillCooldown{total= totalSkill2Cooldown, current= currentSkill2Cooldown});
        }
    }
    public void InvencibilityTimeStart(object sender, PlayerController.CooldownCount cooldown)
    {
        currentInvencibilityTime=cooldown.cooldown;
    }

}
