using UnityEngine;

public class OrcStateMachine : StateMachine<OrcState>
{
    public OrcStateMachine(OrcController orcController)
    {
        RegisterState(new WardOrcState(this,orcController));
        RegisterState(new PatrolOrcState(this,orcController));
        RegisterState(new ChaseOrcState(this,orcController));
        RegisterState(new AttackOrcState(this,orcController));
        RegisterState(new HurtOrcState(this,orcController));
        SwitchState("Ward");
    }
}
