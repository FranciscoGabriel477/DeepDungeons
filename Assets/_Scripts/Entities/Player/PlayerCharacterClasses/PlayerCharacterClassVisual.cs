using UnityEngine;

public class PlayerCharacterClassVisual : EntityVisual
{
    private string airStateName;
    public virtual void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo){}
    public void AirStateChanged(object sender,StateMachine<PlayerAirState>.StateChangeInfo stateChangeInfo)
    {
        airStateName=stateChangeInfo.newState.stateName;
    }

}
