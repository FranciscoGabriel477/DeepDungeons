using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityVisual : MonoBehaviour
{
    private Animator entityAnimator;

    private void Awake()
    {
        entityAnimator=GetComponent<Animator>();
    }
    public void PlayAnimation(string name)
    {
        entityAnimator.Play(name);
    }
    public bool CheckStateName(string name)
    {
        return entityAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
