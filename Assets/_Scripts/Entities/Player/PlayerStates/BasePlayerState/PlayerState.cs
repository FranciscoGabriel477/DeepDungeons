using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerState : State<PlayerState>
{
    protected PlayerController player;
    protected GameInput gameInput;
    public bool invencible{get; protected set;}
    public PlayerState(PlayerStateMachine parent, string stateName,PlayerController player) : base(parent, stateName)
    {
        this.player=player;
        gameInput=GameInput.instance;
  
    }
    protected virtual void HandleHorizontalMomentum() 
    {
        player.SetHorizontalFrameVelocity(player.moveDir.x*player.stats.currentSpeed);
    }
    public override void EntryState()
    {
        
    }
    public override void UpdateState(float deltaTime)
    {
        player.GetInputMoveDir();
        player.HandleEffects();
        player.playerHitBox.HandleHitBox(parent.currentState.invencible || player.timers.InInvencibilityTime());
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        player.HandleExternalForces();
        player.CheckGround();
    }
    public override void ExitState()
    {
        
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
}
