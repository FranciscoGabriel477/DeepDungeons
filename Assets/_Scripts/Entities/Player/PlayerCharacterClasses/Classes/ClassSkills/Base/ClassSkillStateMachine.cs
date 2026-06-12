using UnityEngine;

public class ClassSkillStateMachine : StateMachine<ClassSkillState>
{
    public ClassSkillStateMachine(PlayerController playerController)
    {
        RegisterState(new NotCastingSkillState(this,playerController)); 
        SwitchState(PlayerStateName.NotCastingSkill);
    }
}
