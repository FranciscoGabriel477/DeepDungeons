using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirMeeleAttackState : SoldierAttackState
{
    float actualTime;
    bool canRotate;
    public AirMeeleAttackState(ClassAttackStateMachine parent,PlayerController player) : base(parent, SoldierAttackName.AirMeeleAttack, player)
    {
        notAllowedActions= new HashSet<string>{};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block,PlayerStateName.SkillCast};
    }
    public override void EntryState()
    {
        actualTime=soldier.weaponInfo.attackTimeOnMeele1;
        soldier.soldierVisual.OnInitiateOfMeeleAttack1Animation+=InitiateAirMeeleAttack;
        soldier.soldierVisual.DoAirVisualReserve();
        canRotate=true;
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(PlayerStateName.NotAttacking);
        }
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
        if (canRotate)
        {
            HandleRotation();
        }
    }
    public override void ExitState()
    {
        soldier.soldierVisual.OnInitiateOfMeeleAttack1Animation-=InitiateAirMeeleAttack;
        soldier.soldierVisual.CancelAirVisualReserve();
    }
    public void InitiateAirMeeleAttack(object sender, EventArgs e)
    {
        Attack(player.transform.rotation.eulerAngles.y);
        canRotate=false;
    }
    public virtual void Attack(float dir)
    {
        Vector2 offSet=soldier.transform.rotation.eulerAngles.y==0?soldier.weaponInfo.airAttackMeele1OffSet:-soldier.weaponInfo.airAttackMeele1OffSet;
        offSet.y=soldier.weaponInfo.airAttackMeele1OffSet.y;
        Vector2 attackCenter=(Vector2)soldier.transform.position+offSet;
        Collider2D[] enemiesHitted=Physics2D.OverlapBoxAll(attackCenter,soldier.weaponInfo.airAttackMeele1Size,soldier.transform.rotation.eulerAngles.y,soldier.weaponInfo.enemyLayer);
        foreach(Collider2D enemyHitted in enemiesHitted){
            Vector2 KnockBackDir=soldier.transform.position.x-enemyHitted.transform.position.x<0?Vector2.right:Vector2.left;
            enemyHitted.gameObject.TryGetComponent<IHitable>(out IHitable enemy);
            if(enemy != null)
            {
                enemy.GetHit(new HitInfo{damage=soldier.weaponInfo.damage, knockBack=KnockBackDir*soldier.weaponInfo.knockBackImpulse,posOrigin=soldier.transform.position});
            }
        }
    }
}
