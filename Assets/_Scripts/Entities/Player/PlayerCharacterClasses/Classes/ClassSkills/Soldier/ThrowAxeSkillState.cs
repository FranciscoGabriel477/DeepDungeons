using System;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class ThrowAxeSkillState : SoldierSkillState
{
    float actualTime;
    bool canRotate;
    public ThrowAxeSkillState(ClassSkillStateMachine parent,PlayerController player) : base(parent, SoldierSkillName.ThrowAxe, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block,PlayerStateName.SkillCast};
    }
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        canRotate=true;
        actualTime=soldier.throwAxeInfo.timeToThrowAxe;
        soldier.soldierVisual.OnInitiateOfThrowAxeSkillAnimation+=ThrowAxe;
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(PlayerStateName.NotCastingSkill);
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
        soldier.soldierVisual.OnInitiateOfThrowAxeSkillAnimation-=ThrowAxe;
    }
    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded;
    }

    protected void ThrowAxe(object sender, EventArgs e)
    {
        GameObject throwableAxe=UnityEngine.Object.Instantiate(soldier.throwAxeInfo.throwableAxePrefab,soldier.transform.position+soldier.throwAxeInfo.axeOffSet,soldier.transform.rotation);
        throwableAxe.GetComponent<ThrowableAxe>().SetDir(player.transform.rotation.eulerAngles.y==0?Vector2.right:Vector2.left);
    }
}
