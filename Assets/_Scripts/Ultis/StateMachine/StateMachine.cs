using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachine<T> where T:State<T>
{
    protected Dictionary<string,T> states;
    protected T currentState;

    protected StateMachine()
    {
        this.states=new Dictionary<string,T>();
    }
    public void Action(float deltaTime)
    {
        if(currentState!=null){
            currentState.UpdateState(deltaTime);
        }
    }

    public void FixedAction(float deltaTime)
    {
        if(currentState!=null){
            currentState.FixedUpdateState(deltaTime);
        }
    }
    public T GetState(string name)
    {
        return states[name];
    }
    public string GetActualStateName()
    {
        return currentState.stateName;
    }

    public void RegisterState(T state)
    {
        states.Add(state.stateName,state);
    }
    public void SwitchState(string stateName)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState=states[stateName];
        currentState.EntryState();
    }

    public bool IsActive(T state)
    {
        return currentState==state;
    }

}
