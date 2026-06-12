using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirRangeAttackState : ArcherAttackState
{
    float actualTime;
    bool canRotate;
    public AirRangeAttackState(ClassAttackStateMachine parent,PlayerController player) : base(parent, ArcherAttackName.AirRangeAttack, player)
    {
        notAllowedActions= new HashSet<string>{};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block,PlayerStateName.SkillCast};
    }
    public override void EntryState()
    {
        actualTime=archer.weaponInfo.airAttackTimeOnRange1;
        archer.archerVisual.OnInitiateOfRangeAttack1Animation+=InitiateAirRangeAttack;
        archer.archerVisual.DoAirVisualReserve();
        player.playerAirControlStateMachine.SwitchState(PlayerAirStateName.Suspend);
        player.SetHorizontalFrameVelocity(0);
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
        if (canRotate)
        {
            HandleRotation();
        }
    }
    public override void ExitState()
    {
        archer.archerVisual.OnInitiateOfRangeAttack1Animation-=InitiateAirRangeAttack;
        archer.archerVisual.CancelAirVisualReserve();
        if (player.playerAirControlStateMachine.GetActualStateName()==PlayerAirStateName.Suspend)
        {
            if (!player.IsGrounded)
            {
                player.playerAirControlStateMachine.SwitchState(PlayerAirStateName.FastFall);
            }
            else
            {
                player.playerAirControlStateMachine.SwitchState(PlayerAirStateName.NotInAir);
            }
        }
    }
    public void InitiateAirRangeAttack(object sender, EventArgs e)
    {
        Attack(player.transform.rotation.eulerAngles.y);
        canRotate=false;
    }
    public virtual void Attack(float dir)
    {
        archer.SummomArrow(dir);
    }
}
