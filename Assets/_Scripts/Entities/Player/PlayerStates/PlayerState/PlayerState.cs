using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : State<PlayerState>
{
    protected PlayerController player;
    protected GameInput gameInput;
    public bool canRotate=true;
    public bool canJump=true;
    public bool canAttack=true;
    public PlayerState(PlayerStateMachine parent, string stateName,PlayerController player) : base(parent, stateName)
    {
        this.player=player;
        gameInput=GameInput.instance;
    }
    protected virtual void HandleHorizontalMomentum() 
    {
        player.SetHorizontalFrameVelocity(player.moveDir.x*player.baseMoveStats.moveHorizontalSpeed);
    }
    protected virtual void GameInputEnable()
    {
        gameInput.OnAttackPressed+=player.AttackPressed;
    }

    protected virtual void GameInputDisable()
    {
        gameInput.OnAttackPressed-=player.AttackPressed;
    }

    public virtual bool CheckTrasitionConditions()
    {
        return true;
    }
}
