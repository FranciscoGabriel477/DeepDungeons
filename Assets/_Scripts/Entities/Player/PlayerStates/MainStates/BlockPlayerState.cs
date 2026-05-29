using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerState : PlayerState
{
    public BlockPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Block, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Block};
    }

    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        gameInput.OnBlockReleased+=BlockRealeased;
    }

    public override void UpdateState(float deltaTime)
    {
        
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }
    public override void ExitState()
    {
        gameInput.OnBlockReleased-=BlockRealeased;
    }

    public bool TryBlock(HitInfo hitInfo)
    {
        return (hitInfo.posOrigin.x > player.transform.position.x && player.isFacingRight) || (hitInfo.posOrigin.x < player.transform.position.x && !player.isFacingRight);
    }
    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded && parent.AllowsTransition(PlayerStateName.Block);
    }

    public void BlockRealeased(object sender, EventArgs e)
    {
        parent.SwitchState(player.moveDir.x==0?PlayerStateName.Idle:PlayerStateName.Walk);
        return;
    }
}
