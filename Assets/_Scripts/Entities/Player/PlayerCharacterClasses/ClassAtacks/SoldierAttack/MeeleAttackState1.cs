using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleAttackState1 : SoldierAttackState
{
    float actualTime;
    bool canRotate;
    public MeeleAttackState1(ClassAttackStateMachine parent,PlayerController player) : base(parent, SoldierAttackName.MeeleAttack1, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block};
    }
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        actualTime=soldier.weaponInfo.attackTimeOnMeele1;
        soldier.soldierVisual.OnInitiateOfMeeleAttack1Animation+=InitiateMeeleAttack1;
        canRotate=true;
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState("NotAttack");
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
        soldier.soldierVisual.OnInitiateOfMeeleAttack1Animation-=InitiateMeeleAttack1;
    }
    public void InitiateMeeleAttack1(object sender, EventArgs e)
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
