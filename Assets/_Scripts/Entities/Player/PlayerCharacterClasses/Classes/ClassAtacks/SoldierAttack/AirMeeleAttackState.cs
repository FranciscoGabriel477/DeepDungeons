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
        RaycastHit2D[] enemiesHitted=new RaycastHit2D[5];
        soldier.weaponCollider.Cast(Vector3.zero,soldier.contactFilter,enemiesHitted);
        if (enemiesHitted[0])
        {
            Vector2 KnockBackDir=soldier.transform.position.x-enemiesHitted[0].transform.position.x<0?Vector2.right:Vector2.left;
            IHitable enemy=null;
            enemiesHitted[0].collider.gameObject.TryGetComponent<IHitable>(out enemy);
            if (enemy != null)
            {
                enemy.GetHit(new HitInfo{damage=soldier.weaponInfo.damage, knockBack=KnockBackDir*soldier.weaponInfo.knockBackImpulse,posOrigin=soldier.transform.position});
            }
        }
    }
}
