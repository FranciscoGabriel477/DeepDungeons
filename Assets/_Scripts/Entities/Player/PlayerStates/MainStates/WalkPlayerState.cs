using UnityEngine;

public class WalkPlayerState : PlayerState
{

    public WalkPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, "Walk", player){}
    public override void EntryState()
    {
        GameInputEnable();
    }

    public override void UpdateState(float deltaTime)
    {
        if (player.moveDir.x == 0)
        {
            parent.SwitchState("Idle");
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        HandleHorizontalMomentum();
    }

    public override void ExitState()
    {
        GameInputDisable();
    }

    protected override void GameInputEnable()
    {
        gameInput.OnAttackPressed+=player.AttackPressed;
    }

    protected override void GameInputDisable()
    {
        gameInput.OnAttackPressed-=player.AttackPressed;
    }

    
}
