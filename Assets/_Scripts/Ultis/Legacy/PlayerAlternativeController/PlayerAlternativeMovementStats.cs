using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Alternative Moviment Stats")]
public class PlayerAlternativeMovementStats : ScriptableObject
{
    public float maxWalkSpeed=12.5f;
    public float groundAceleration=5f;
    public float groundDeceleration=20f;
    public float airAceleration=5f;
    public float airDeceleration=5f;
    public float maxRunSpeed=20f;
    public LayerMask groundLayer;
    public float groundDetectionRayLenght=0.02f;
    public float headDetectionRayLenght=0.02f;
    public float headWidth=0.75f;
    public float jumpHeight=6.5f;
    public float timeTillApex=1.5f;
    public float gravityMultiplierOnJumpRelease=3f;
    public float maxFallSpeed=3f;
    public int numberOfJumpsAllowed=2;
    public float timeForUpwardsCancel=0.027f;
    public float apexThresHold=0.97f;
    public float apexHangTime=0.075f;
    public float jumpBufferTime=0.125f;
    public float jumpCoyoteTime=0.1f;
    public float gravity{get; private set;}
    public float initialJumpVelocity{get; private set;}
    private void OnValidate()
    {
        CalculateValues();
    }


    private void OnEnable()
    {
        CalculateValues();        
    }
    private void CalculateValues()
    {
        gravity= (float)(-(2*jumpHeight)/Math.Pow(timeTillApex,2f));
        initialJumpVelocity=Math.Abs(gravity)*timeTillApex;
    }
}
