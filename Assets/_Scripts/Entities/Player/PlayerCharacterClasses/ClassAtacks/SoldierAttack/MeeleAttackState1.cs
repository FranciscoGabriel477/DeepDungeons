using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleAttackState1 : SoldierAttackState
{
    float actualTime;
    public MeeleAttackState1(ClassAttackStateMachine parent,PlayerController player) : base(parent, SoldierAttackName.MeeleAttack1,player){}
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        actualTime=soldier.weaponInfo.attackTimeOnMeele1;
        soldier.soldierVisual.OnInitiateOfMeeleAttack1Animation+=InitiateMeeleAttack1;
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState("NotAttack");
        }
    }
    public override void ExitState()
    {
        soldier.soldierVisual.OnInitiateOfMeeleAttack1Animation-=InitiateMeeleAttack1;
    }
    public void InitiateMeeleAttack1(object sender, EventArgs e)
    {
        Attack(player.transform.rotation.eulerAngles.y);
    }
    public virtual void Attack(float dir)
    {
        RaycastHit2D[] enemiesHitted=new RaycastHit2D[5];
        soldier.weaponCollider.Cast(Vector3.zero,soldier.contactFilter,enemiesHitted);
        if (enemiesHitted[0])
        {
            Vector2 KnockBackDir=soldier.transform.position.x-enemiesHitted[0].transform.position.x<0?Vector2.right:Vector2.left;
            enemiesHitted[0].collider.gameObject.GetComponent<IHitable>().GetHit(new HitInfo{damage=soldier.weaponInfo.damage, knockBack=KnockBackDir*soldier.weaponInfo.knockBackImpulse,posOrigin=soldier.transform.position});
        }
    }
}
