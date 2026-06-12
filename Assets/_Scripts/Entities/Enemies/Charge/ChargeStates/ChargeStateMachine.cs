using UnityEngine;

public class ChargeStateMachine : EnemyStateMachine
{
    public ChargeStateMachine(ChargeController chargeController)
    {
        RegisterState(new WardChargeState(this,chargeController));
        RegisterState(new PatrolChargeState(this,chargeController));
        RegisterState(new ChaseChargeState(this,chargeController));
        RegisterState(new HurtChargeState(this,chargeController));
        RegisterState(new GoBackChargeState(this,chargeController));
        RegisterState(new CoolingChargeState(this,chargeController));
        RegisterState(new AttackChargeState(this,chargeController));
        RegisterState(new FastAttackChargeState(this,chargeController));
        SwitchState(ChargeStateName.Ward);
    }
}
