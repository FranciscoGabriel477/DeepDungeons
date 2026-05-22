using UnityEngine;

public abstract class State<T> where T: State<T>
{
    protected  StateMachine<T> parent;
    public readonly string stateName;

    public State(StateMachine<T> parent, string stateName)
    {
        this.parent=parent;
        this.stateName=stateName;
    }
    public abstract void EntryState();
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);
    public abstract void ExitState();

}
