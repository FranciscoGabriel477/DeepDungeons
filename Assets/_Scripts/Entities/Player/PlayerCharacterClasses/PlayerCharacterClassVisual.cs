using UnityEngine;

public class PlayerCharacterClassVisual : EntityVisual
{
    protected string airStateName;
    public virtual void MainStateChanged(object sender,StateMachine<PlayerState>.StateChangeInfo stateChangeInfo){}
    public virtual void AirStateChanged(object sender,StateMachine<PlayerAirState>.StateChangeInfo stateChangeInfo)
    {
        airStateName=stateChangeInfo.newState.stateName;
        if (mainStateName!= PlayerStateName.Idle && mainStateName!= PlayerStateName.Walk)
        {
            return;
        }
        switch(airStateName){
            case PlayerAirStateName.NotInAir:
                animator.Play(mainStateName);
                break;
            case PlayerAirStateName.Jump:
                animator.Play(PlayerAirStateName.Jump);
            break;
            case PlayerAirStateName.FastFall:
                animator.Play(PlayerAirStateName.Fall);
            break;
            case PlayerAirStateName.Fall:
                animator.Play(PlayerAirStateName.Fall);
                break;
            default:
            break;
        }
    }

}
