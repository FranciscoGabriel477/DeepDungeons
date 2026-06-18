using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerState : PlayerState
{
    public BlockPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, PlayerStateName.Block, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Block,PlayerStateName.SkillCast};
    }

    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        gameInput.OnBlockReleased+=BlockRealeased;
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        base.FixedUpdateState(fixedDeltaTime);
        HandleRotation();
        player.Move();
    }
    public override void ExitState()
    {
        gameInput.OnBlockReleased-=BlockRealeased;
        player.ResetBlockCooldown();
    }

    public bool TryBlock(HitInfo hitInfo)
    {
        return (hitInfo.posOrigin.x > player.transform.position.x && player.isFacingRight) || (hitInfo.posOrigin.x < player.transform.position.x && !player.isFacingRight);
    }
    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded && parent.AllowsTransition(PlayerStateName.Block) && !player.timers.BlockInCooldown();
    }

    public void BlockRealeased(object sender, EventArgs e)
    {
        parent.SwitchState(player.moveDir.x==0?PlayerStateName.Idle:PlayerStateName.Walk);
        return;
    }
}
