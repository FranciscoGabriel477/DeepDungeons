using System;
using UnityEngine;

public class EnemyState : State<EnemyState>
{
    public EnemyState(StateMachine<EnemyState> parent, string stateName) : base(parent, stateName)
    {
    
    }
}
