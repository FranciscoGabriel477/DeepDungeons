using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeCutSkillState : SoldierSkillState
{
    float actualTime;
    float chargeTime;
    bool canRotate;
    bool chargeEnd=false;
    public ChargeCutSkillState(ClassSkillStateMachine parent,PlayerController player) : base(parent, SoldierSkillName.ChargeCut, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block,PlayerStateName.SkillCast};
    }
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        actualTime=soldier.chargeCutInfo.finishCutTime;
        soldier.soldierVisual.OnInitiateOfChargeCutSkillAnimation+=InitiateChargeCut;
        canRotate=true;
        chargeEnd=false;
        chargeTime=0;
    }
    public override void UpdateState(float deltaTime)
    {
        if (chargeEnd)
        {
            actualTime-=deltaTime;
            if (actualTime <= 0)
            {
                parent.SwitchState(PlayerStateName.NotCastingSkill);
            }  
        }
        else
        {
            chargeTime=math.min(chargeTime+deltaTime,soldier.chargeCutInfo.timeToTotalDamge);
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        if (canRotate)
        {
            HandleRotation();
        }
    }
    public override void ExitState()
    {
        soldier.soldierVisual.OnInitiateOfChargeCutSkillAnimation-=InitiateChargeCut;
    }
    public override void HabilityButttonRealized(object sender, EventArgs e)
    {
        if (!chargeEnd)
        {
            canRotate=false;
            chargeEnd=true;
            soldier.soldierVisual.PlayChargeCutFinishAnimation();
        }
    }
    public void InitiateChargeCut(object sender, EventArgs e)
    {
        ChargeCutCast(player.transform.rotation.eulerAngles.y);
    }
    public virtual void ChargeCutCast(float dir)
    {
        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll((Vector2)soldier.transform.position+(dir==0f?soldier.chargeCutInfo.colliderOffset:-soldier.chargeCutInfo.colliderOffset),soldier.chargeCutInfo.colliderSize,0f,soldier.weaponInfo.enemyLayer);
        if (enemiesHitted.Length>0)
        {
            IHitable enemy;
            enemiesHitted[0].gameObject.TryGetComponent<IHitable>(out enemy);
            if (enemy != null)
            {
                float totalDamage=Mathf.InverseLerp(0f,soldier.chargeCutInfo.timeToTotalDamge,chargeTime)*soldier.chargeCutInfo.chargeDamage+ soldier.chargeCutInfo.baseDamage;
                float totalKnockBack=Mathf.InverseLerp(0f,soldier.chargeCutInfo.timeToTotalDamge,chargeTime)*soldier.chargeCutInfo.chargeKnockBack+ soldier.chargeCutInfo.baseKnockBack;
                Vector2 KnockBackDir=soldier.transform.position.x-enemiesHitted[0].transform.position.x<0?Vector2.right:Vector2.left;
                enemy.GetHit(new HitInfo{damage=totalDamage,knockBack=KnockBackDir*totalKnockBack,posOrigin=soldier.transform.position});
            }
        }
    }

    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded;
    }
}
