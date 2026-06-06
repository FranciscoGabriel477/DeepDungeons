using UnityEngine;

public class BeeStateMachine : EnemyStateMachine
{
    public BeeStateMachine(BeeController BeeController)
    {
        RegisterState(new WardBeeState(this,BeeController));
        RegisterState(new PatrolBeeState(this,BeeController));
        RegisterState(new ChaseBeeState(this,BeeController));
        RegisterState(new AttackBeeState(this,BeeController));
        RegisterState(new HurtBeeState(this,BeeController));
        RegisterState(new GoBackBeeState(this,BeeController));
        RegisterState(new CoolingBeeState(this,BeeController));
        SwitchState(BeeStateName.Ward);
    }
}
