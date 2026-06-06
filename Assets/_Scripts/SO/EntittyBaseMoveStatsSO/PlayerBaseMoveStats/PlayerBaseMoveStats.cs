using UnityEngine;

[CreateAssetMenu(menuName ="Player Base Moviment Stats")]

public class PlayerBaseMoveStats : EntityBaseMoveStats
{
    public float timeTillApex;
    public float timeInDashState;
    public float jumpHeight;
    public float minJumpHeight;
    public float maxVerticalSpeed;
    public float gravityMultiplierOnJumpRelease;
    public float apexThresHold;
    public float timeInApexPoint;
    public float timeInFastFallingTrasition;
    public float jumpBufferTimer;
    public float minVerticalJumpVelocity;
    public float jumpInitialSpeed;
    public float dashSpeed;
    public float dashCooldown;
    public float dashStaminaCost;
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
        gravityAcc= (float)(-(2*jumpHeight)/Mathf.Pow(timeTillApex,2f));
        jumpInitialSpeed=Mathf.Abs(gravityAcc)*timeTillApex;
        minVerticalJumpVelocity=Mathf.Sqrt(Mathf.Pow(jumpInitialSpeed,2)+2*gravityAcc*minJumpHeight);
    }
}
