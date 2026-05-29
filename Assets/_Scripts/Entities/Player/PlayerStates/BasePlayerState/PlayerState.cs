using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : State<PlayerState>
{
    protected PlayerController player;
    protected GameInput gameInput;
    public PlayerState(PlayerStateMachine parent, string stateName,PlayerController player) : base(parent, stateName)
    {
        this.player=player;
        gameInput=GameInput.instance;
  
    }
    protected virtual void HandleHorizontalMomentum() 
    {
        player.SetHorizontalFrameVelocity(player.moveDir.x*player.baseMoveStats.moveHorizontalSpeed);
    }
    

    public virtual bool CheckTrasitionConditions()
    {
        return true;
    }
}
