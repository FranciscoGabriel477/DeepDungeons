using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : EntityVisual
{
    public event EventHandler OnInitiateOfAttackAnimation;
    public event EventHandler OnEndOfAttackAnimation;
    
    public void InitiateOfAttackAnimation()
    {
        OnInitiateOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }

    public void EndOfAttackAnimation()
    {
        OnEndOfAttackAnimation?.Invoke(this,EventArgs.Empty);
    }
}
