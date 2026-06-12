using System;
using Unity.Services.Analytics;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityVisual : MonoBehaviour
{
    protected string mainStateName;
    protected Animator animator;
    public SpriteRenderer entitySprite;

    private void Awake()
    {
        animator=GetComponent<Animator>();
        entitySprite=GetComponent<SpriteRenderer>();
    }
    /*public virtual void PlayAnimation(string name)
    {
        animator.Play(name);
    }*/
    public virtual bool CheckStateName(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public virtual void SetMainStateName(string stateName)
    {
        mainStateName=stateName;
    }
}
