using UnityEngine;

public class OrcStateMachine : EnemyStateMachine
{
    public OrcStateMachine(OrcController orcController)
    {
        RegisterState(new WardOrcState(this,orcController));
        RegisterState(new PatrolOrcState(this,orcController));
        RegisterState(new ChaseOrcState(this,orcController));
        RegisterState(new AttackOrcState(this,orcController));
        RegisterState(new HurtOrcState(this,orcController));
        RegisterState(new GoBackOrcState(this,orcController));
        RegisterState(new CoolingOrcState(this,orcController));
        SwitchState(OrcStateName.Ward);
    }
}
