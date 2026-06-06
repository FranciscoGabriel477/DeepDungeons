using UnityEngine;

public class BeeState : EnemyStateWithComponents<BeeController, BeeMover, BeeVisual, BeeStats, BeeStateMachine, BeeBaseStats, BeeBaseMoveStats>
{
    public BeeController bee;
    public BeeState(EnemyStateMachine parent, string stateName, BeeController bee) : base(parent, stateName, bee)
    {
        this.bee=bee;
    }
}
