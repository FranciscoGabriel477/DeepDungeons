using System;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class ToxicArrowSkillState :ArcherSkillState
{
    float actualTime;
    bool canRotate;
    public ToxicArrowSkillState(ClassSkillStateMachine parent,PlayerController player) : base(parent, ArcherSkillName.ToxicArrow, player)
    {
        notAllowedActions= new HashSet<string>{PlayerActionName.Jump};
        notAllowedTransitions = new HashSet<string>{PlayerStateName.Dash,PlayerStateName.Attack,PlayerStateName.Block,PlayerStateName.SkillCast};
    }
    public override void EntryState()
    {
        player.SetHorizontalFrameVelocity(0);
        canRotate=true;
        actualTime=archer.toxicArrowInfo.timeToShotToxicArrow;
        archer.archerVisual.OnInitiateOfToxicArrowSkillAnimation+=ShotToxicArrow;
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
        archer.archerVisual.OnInitiateOfToxicArrowSkillAnimation-=ShotToxicArrow;
    }
    public override bool CheckTrasitionConditions()
    {
        return player.IsGrounded;
    }

    protected void ShotToxicArrow(object sender, EventArgs e)
    {
        GameObject toxicArrow=UnityEngine.Object.Instantiate(archer.toxicArrowInfo.toxicArrowPrefab,archer.transform.position+archer.toxicArrowInfo.toxicArrowOffSet,archer.transform.rotation);
        toxicArrow.GetComponent<PlayerToxicArrow>().direction=player.transform.rotation.eulerAngles.y==0?Vector2.right:Vector2.left;
    }
}
