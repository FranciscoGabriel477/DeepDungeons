using UnityEngine;

[CreateAssetMenu(menuName ="Player Base Moviment Stats")]

public class PlayerBaseMoveStats : ScriptableObject
{
    public float moveHorizontalSpeed;
    public float timeTillApex;
    public float jumpHeight;
    public float minJumpHeight;
    public float maxVerticalSpeed;
    public float gravityMultiplierOnJumpRelease;
    public float apexThresHold;
    public float timeInApexPoint;
    public float timeInFastFallingTrasition;
    public float jumpBufferTimer;
    public float minVerticalJumpVelocity;
    public float gravityAcc;
    public float jumpInitialSpeed;
    public float knockBackDecelaration;
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
