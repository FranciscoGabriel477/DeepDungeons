using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : State<PlayerAirState>
{
    protected PlayerController player;
    protected GameInput gameInput;
    public PlayerAirState(PlayerAirControlStateMachine parent, string stateName,PlayerController player) : base(parent, stateName)
    {
        this.player=player;
        gameInput=GameInput.instance;
    }
    protected virtual void HandleVerticalMomentum(float fixedDeltaTime)
    {
        
    }
}
