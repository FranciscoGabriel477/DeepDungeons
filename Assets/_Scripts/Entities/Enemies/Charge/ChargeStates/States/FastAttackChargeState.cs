using System;
using UnityEngine;

public class FastAttackChargeState : ChargeState
{
    float actualTime;
    int numberOfAttackInList=1;
    public FastAttackChargeState(ChargeStateMachine parent, ChargeController charge) : base(parent, ChargeStateName.FastAttack, charge)
    {
    }
    public override void EntryState()
    {
        base.EntryState();
        actualTime=enemy.stats.baseStats.attackDatas[numberOfAttackInList].attackTime;
        charge.visual.OnInitiateOfFastAttackAnimation+=InitiateOfFastAttackAnimation;
        HandleMoveDir();
        HandleRotation();
        enemy.SetHorizontalFrameVelocity(0f);
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(ChargeStateName.Cooling);
            return;
        }
        if (!IsBounded())
        {
            parent.SwitchState(ChargeStateName.GoBack);
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
    }

    public override void ExitState()
    {
        base.ExitState();
        charge.visual.OnInitiateOfFastAttackAnimation-=InitiateOfFastAttackAnimation;
        enemy.attacksCooldowns[numberOfAttackInList]=enemy.stats.baseStats.attackDatas[numberOfAttackInList].cooldown;
    }
    protected void InitiateOfFastAttackAnimation(object sender, EventArgs e)
    {
        Attack(charge.transform.rotation.eulerAngles.y);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.Normalize(enemy.GetPlayerPos()-enemy.transform.position);
    }
    public virtual void Attack(float dir)
    {
        Vector2 offset=dir==0?charge.chargeWeapon.weaponInfo.fastAttackOffSet:-charge.chargeWeapon.weaponInfo.fastAttackOffSet;
        offset.y=charge.chargeWeapon.weaponInfo.fastAttackOffSet.y;
        Vector2 attackCenter=(Vector2)charge.transform.position+offset;
        Collider2D[] enemiesHitted=Physics2D.OverlapBoxAll(attackCenter,charge.chargeWeapon.weaponInfo.fastAttackSize,dir,charge.chargeWeapon.weaponInfo.enemyLayer);
        foreach(Collider2D enemyHitted in enemiesHitted){
            Vector2 KnockBackDir=charge.transform.position.x-enemyHitted.transform.position.x<0?Vector2.right:Vector2.left;
            enemyHitted.gameObject.TryGetComponent<IHitable>(out IHitable enemy);
            if(enemy != null)
            {
                enemy.GetHit(new HitInfo{damage=charge.chargeWeapon.weaponInfo.damage, knockBack=KnockBackDir*charge.chargeWeapon.weaponInfo.knockBackImpulse,posOrigin=charge.transform.position});
            }
        }
    }
}
