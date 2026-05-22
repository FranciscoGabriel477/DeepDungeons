using UnityEngine;

public class OrcStateMachine : StateMachine<OrcState>
{
    public OrcStateMachine(OrcController orcController)
    {
        RegisterState(new IdleOrcState(this,orcController));
        RegisterState(new WalkOrcState(this,orcController));
        RegisterState(new AttackOrcState(this,orcController));
        RegisterState(new HurtOrcState(this,orcController));
        SwitchState("Idle");
    }
}
