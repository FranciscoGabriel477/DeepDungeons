using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashPlayerState : PlayerState
{
    private float actualTime;
    public DashPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Dash, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump,PlayerActionName.Rotate};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Attack,PlayerStateName.Dash,PlayerStateName.Block};
    }

    public override void EntryState()
    {
        player.stats.ConsumeStamina(player.stats.baseMoveStats.dashStaminaCost);
        player.playerHitBox.enabled=false;
        actualTime=player.stats.baseMoveStats.timeInDashState;
        float dashDir;
        if (player.moveDir.x != 0)
        {
            dashDir=player.moveDir.x;
        }
        else
        {
            dashDir=player.transform.rotation.eulerAngles.y==0?1f:-1f;
        }
        player.SetHorizontalFrameVelocity(dashDir*player.stats.baseMoveStats.dashSpeed);

        if (!player.IsGrounded)
        {
            player.playerAirControlStateMachine.SwitchState(PlayerAirStateName.Suspend);
        }
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
        player.playerHitBox.enabled=true;
        player.ResetDashCooldown();
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

    public override bool CheckTrasitionConditions()
    {
        return parent.AllowsTransition(PlayerStateName.Dash) && player.currentTimerDashCoolDown==0 && player.stats.currentStamina>=player.stats.baseMoveStats.dashStaminaCost;
    }
}
