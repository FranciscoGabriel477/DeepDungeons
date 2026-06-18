using System;
using UnityEngine;

public class AttackOrcState : OrcState
{
    private float actualTime;
    private int numberOfAttackInList=0;
    public AttackOrcState(OrcStateMachine parent,OrcController orc) : base(parent, OrcStateName.Attack, orc){}

    public override void EntryState()
    {
        orc.SetHorizontalFrameVelocity(0);
        actualTime=orc.stats.baseStats.attackDatas[numberOfAttackInList].attackTime;
        orc.visual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
    }
    public override void UpdateState(float deltaTime)
    {
        HandleMoveDir();
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(OrcStateName.Cooling);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }
    public override void ExitState()
    {
        enemy.attacksCooldowns[numberOfAttackInList]=enemy.stats.baseStats.attackDatas[numberOfAttackInList].cooldown;
        orc.visual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
    }   
    private void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        Attack(orc.transform.rotation.eulerAngles.y);
    }
    protected override void HandleMoveDir()
    {
        orc.moveDir=Vector3.Normalize(orc.GetPlayerPos().x*Vector3.right-orc.transform.position);
    }
    public virtual void Attack(float dir)
    {
        Vector2 offset=dir==0?orc.orcWeapon.weaponInfo.attack1OffSet:-orc.orcWeapon.weaponInfo.attack1OffSet;
        offset.y=orc.orcWeapon.weaponInfo.attack1OffSet.y;
        Vector2 attackCenter=(Vector2)orc.transform.position+offset;
        Collider2D[] enemiesHitted=Physics2D.OverlapBoxAll(attackCenter,orc.orcWeapon.weaponInfo.attack1Size,dir,orc.orcWeapon.weaponInfo.enemyLayer);
        foreach(Collider2D enemyHitted in enemiesHitted){
            Vector2 KnockBackDir=orc.transform.position.x-enemyHitted.transform.position.x<0?Vector2.right:Vector2.left;
            enemyHitted.gameObject.TryGetComponent<IHitable>(out IHitable enemy);
            if(enemy != null)
            {
                enemy.GetHit(new HitInfo{damage=orc.orcWeapon.weaponInfo.damage, knockBack=KnockBackDir*orc.orcWeapon.weaponInfo.knockBackImpulse,posOrigin=orc.transform.position});
            }
        }
    }
}
