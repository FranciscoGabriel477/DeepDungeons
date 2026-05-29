using UnityEngine;

public class ClassAttackStateMachine : StateMachine<ClassAttackState>
{
    public ClassAttackStateMachine(PlayerController playerController)
    {
        RegisterState(new NotAttackState(this,playerController)); 
        SwitchState("NotAttack");
    }
}
