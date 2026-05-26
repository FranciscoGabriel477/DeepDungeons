using UnityEngine;

public abstract class State<T> where T: State<T>
{
    protected  StateMachine<T> parent;
    public string stateName;

    public State(StateMachine<T> parent, string stateName)
    {
        this.parent=parent;
        this.stateName=stateName;
    }
    public virtual void EntryState(){}
    public virtual void UpdateState(float deltaTime){}
    public virtual void FixedUpdateState(float fixedDeltaTime){}
    public virtual void ExitState(){}

}
