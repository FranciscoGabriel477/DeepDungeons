using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackPlayerState : PlayerState
{
    public AttackPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, "Attack", player)
    {
        canRotate=false;
        canJump=false;
    }
    float attackTime=0.5f;
    float actualTime;
    public override void EntryState()
    {
    
        player.SetHorizontalFrameVelocity(0);
        player.playerVisual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
        player.playerVisual.OnEndOfAttackAnimation+=EndOfAttackAnimation;
        //player.playerVisual.PlayAnimation("Attack1");
        actualTime=attackTime;
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(player.moveDir.x==0?"Idle":"Walk");
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void ExitState()
    {
        player.playerVisual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;
        player.playerVisual.OnEndOfAttackAnimation-=EndOfAttackAnimation;
    }

    private void EndOfAttackAnimation(object sender, EventArgs e){
        parent.SwitchState(player.moveDir.x==0?"Idle":"Walk");
    }
    private void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        player.playerWeapon.Attack(player.transform.rotation.eulerAngles.y);
    }

    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded && player.playerAirControlStateMachine.GetActualStateName()=="NotInAir" && parent.currentState.canAttack;
    }
    
}


