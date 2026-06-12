using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassSkillState : State<ClassSkillState>
{
    protected PlayerController player;
    protected GameInput gameInput;
    public ClassSkillState(ClassSkillStateMachine parent, string stateName,PlayerController player) : base(parent, stateName)
    {
        this.player=player;
        gameInput=GameInput.instance;
  
    }
    protected virtual void HandleHorizontalMomentum() 
    {
        player.SetHorizontalFrameVelocity(player.moveDir.x*player.stats.currentSpeed);
    }
    
    protected virtual void HandleRotation()
    {
        if (player.isFacingRight && player.moveDir.x<0)
        {
            player.isFacingRight=false;
            player.transform.Rotate(0,-180f,0);
        }
        else if(!player.isFacingRight && player.moveDir.x>0)
        {
            player.isFacingRight=true;
            player.transform.Rotate(0,180f,0);
        }
    }
    public virtual bool CheckTrasitionConditions()
    {
        return true;
    }

    public virtual void HabilityButttonRealized(object sender, EventArgs e)
    {
        
    }
}
