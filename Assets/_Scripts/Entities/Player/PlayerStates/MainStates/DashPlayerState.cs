using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashPlayerState : PlayerState
{
    private float actualTime;
    public DashPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Dash, player)
    {
        canJump=false;
        canRotate=false;
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Attack,PlayerStateName.Dash};
    }

    public override void EntryState()
    {
        actualTime=player.baseMoveStats.timeInDashState;
        float dashDir;
        if (player.moveDir.x != 0)
        {
            dashDir=player.moveDir.x;
        }
        else
        {
            dashDir=player.transform.rotation.eulerAngles.y==0?1f:-1f;
        }
        player.SetHorizontalFrameVelocity(dashDir*player.baseMoveStats.dashSpeed);
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

    public override bool CheckTrasitionConditions()
    {
        return parent.AllowsTransition(PlayerStateName.Dash);
    }
}
