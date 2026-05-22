using UnityEngine;

public class HurtPlayerState : PlayerState
{
    private float actualTime;
    public HurtPlayerState(PlayerStateMachine parent,PlayerController player) : base(parent, "Hurt", player)
    {
        canJump=false;
        canAttack=false;
        canRotate=false;
    }

    public override void EntryState()
    {
        actualTime=player.baseStats.timeInHurtState;
        player.SetHorizontalFrameVelocity(0);
    }
    public override void UpdateState(float deltaTime)
    {
        actualTime-=Time.deltaTime;
        if (actualTime <= 0)
        {
            parent.SwitchState(player.moveDir.x==0?"Idle":"Walk");
            return;
        }
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }
    public override void ExitState()
    {
        
    }
}
