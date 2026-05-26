using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackPlayerState : PlayerState
{
    float attackTime=0.5f;
    float actualTime=0f;
    public AttackPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Attack, player)
    {
        canRotate=false;
        canJump=false;
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack};
    }
    
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        player.playerVisual.OnInitiateOfAttackAnimation+=InitiateOfAttackAnimation;
        actualTime=attackTime;
    }

    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(player.moveDir.x==0?PlayerStateName.Idle:PlayerStateName.Walk);
            return;
        }
    }
    public override void ExitState()
    {
        player.playerVisual.OnInitiateOfAttackAnimation-=InitiateOfAttackAnimation;

    }
    private void InitiateOfAttackAnimation(object sender, EventArgs e)
    {
        player.playerWeapon.Attack(player.transform.rotation.eulerAngles.y);
    }
    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded && player.playerAirControlStateMachine.GetActualStateName()== PlayerAirStateName.NotInAir && parent.AllowsTransition(PlayerStateName.Attack);
    }
    
}


