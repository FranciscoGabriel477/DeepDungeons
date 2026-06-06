using UnityEngine;

public class CoolingBeeState : CoolingState<BeeController,BeeMover,BeeVisual,BeeStats,BeeStateMachine,BeeBaseStats,BeeBaseMoveStats>
{
    public CoolingBeeState(BeeStateMachine parent, BeeController enemy) : base(parent, enemy){}

    public override void EntryState()
    {
        base.EntryState();
        HandleMoveDir();
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       base.FixedUpdateState(fixedDeltaTime);
       HandleHorizontalMomentum();
       HandleVerticalMomentum(fixedDeltaTime);
       HandleRotation();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    protected override void HandleVerticalMomentum(float fixedDeltaTime)
    {
        enemy.SetVerticalFrameVelocity(enemy.moveDir.y*enemy.stats.baseMoveStats.coolingVelocity);
    }

    protected override void HandleMoveDir()
    {
        enemy.moveDir=Vector3.up;
    }

    protected override void HandleRotation()
    {
        Vector3 playerDir=(enemy.GetPlayerPos()-enemy.transform.position).normalized;
        if (enemy.isFacingRight && playerDir.x<0)
        {
            enemy.isFacingRight=false;
            enemy.transform.Rotate(0,-180f,0);
        }
        else if(!enemy.isFacingRight && playerDir.x>0)
        {
            enemy.isFacingRight=true;
            enemy.transform.Rotate(0,180f,0);
        }
    }
}
