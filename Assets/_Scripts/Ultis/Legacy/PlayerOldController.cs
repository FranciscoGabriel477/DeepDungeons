using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerOldController : MonoBehaviour
{
    public PlayerMover playerMover{get; private set;} 
   // public PlayerVisual playerVisual{get; private set;}  
    public GameInput gameInput{get; private set;}  
    public Vector2 moveDir{get; private set;} 
    public Vector2 frameVelocity;
    [SerializeField] private PlayerBaseMoveStats baseMoveStats;
    [SerializeField] private float moveHorizontalSpeed;
    private float verticalVelocityInJumpHeld=0;
    private float jumpBufferCurrentTimer=0;
    private float currentTimerInApexPoint=0;
    private float currentTimerInFastFallingTrasition=0;
    private bool isjumping=false;
    private bool jumpIsPressed=false;
    private bool jumpIsHelded=true;
    private bool isFastFalling=false;
    //private bool jumpCancel=false;
    private bool isFastFallingTransition=false;
    private bool isInApexPoint=false;
    private bool isFacingRight=true;
    private bool isGrounded=true;
    public void Awake()
    {
        playerMover=GetComponent<PlayerMover>();
        //playerVisual=GetComponentInChildren<PlayerVisual>();
    }

    private void Start()
    {
        gameInput = GameInput.instance;
        gameInput.OnJumpPressed+=JumpPressed;
        gameInput.OnJumpHelded+=JumpHelded;
    }

    public void Update()
    {
        HandleJumpChecks();
        CountTimers();
        moveDir=gameInput.GetNormalizedMovementInput();
        HandleVisual();
    }

    public void FixedUpdate()
    {
        isGrounded=playerMover.CheckGround();
        HandleHorizontalMomentum();
        HandleVerticalMomentum();
        HandleRotation();
        playerMover.SetVelocity(frameVelocity);
    }
    private void HandleVisual()
    {
        //playerVisual.PlayAnimation(frameVelocity==Vector2.zero?"Idle":"Walk");
    }
    private void HandleRotation()
    {
        if (isFacingRight && moveDir.x<0)
        {
            isFacingRight=false;
            transform.Rotate(0,-180f,0);
        }
        else if(!isFacingRight && moveDir.x>0)
        {
            isFacingRight=true;
            transform.Rotate(0,180f,0);
        }
    }
    private void HandleJumpChecks()
    {
        // if (isjumping && jumpIsHelded)
        // {
        //     jumpCancel=true;
        // }
        if( isFastFalling && isGrounded)
        {
            isjumping=false;
            isFastFalling=false;
            isFastFallingTransition=false;
            isInApexPoint=false;
            //jumpCancel=false;
            currentTimerInApexPoint=0;
            currentTimerInFastFallingTrasition=0;
        }

        if(isjumping && jumpIsHelded && frameVelocity.y < baseMoveStats.minVerticalJumpVelocity)
        {
            //jumpCancel=false;
            if (isInApexPoint)
            {
                isFastFalling=true;
                isInApexPoint=false;
                isjumping=false;
            }
            else
            {
                isFastFallingTransition=true;
                verticalVelocityInJumpHeld=frameVelocity.y;
                currentTimerInFastFallingTrasition=baseMoveStats.timeInFastFallingTrasition;
                isjumping=false;
            }
        }

        if(isjumping && frameVelocity.y<0)
        {
            isFastFalling=true;
            isjumping=false;
        }

        if (CheckJumpConditions())
        {
            InitiateJump();
        }
    }

    private void HandleVerticalMomentum()
    {
        if (isjumping)
        {
            if (Mathf.InverseLerp(baseMoveStats.jumpInitialSpeed,0f,frameVelocity.y)>baseMoveStats.apexThresHold && !isInApexPoint)
            {
                isInApexPoint=true;
                currentTimerInApexPoint=baseMoveStats.timeInApexPoint;
                frameVelocity.y=0;
            } 

            if(currentTimerInApexPoint <= 0 && isInApexPoint)
            {
                isInApexPoint=false;
                frameVelocity.y=-0.01f;
            }

            if(!isInApexPoint)
            {
                frameVelocity.y+=baseMoveStats.gravityAcc*Time.fixedDeltaTime;
            } 
        }

        else if (isFastFallingTransition)
        {
            frameVelocity.y=Mathf.Lerp(verticalVelocityInJumpHeld,0,(baseMoveStats.timeInFastFallingTrasition-currentTimerInFastFallingTrasition)/baseMoveStats.timeInFastFallingTrasition);
            if (currentTimerInFastFallingTrasition <= 0)
            {
                frameVelocity.y+=baseMoveStats.gravityAcc*Time.fixedDeltaTime*baseMoveStats.gravityMultiplierOnJumpRelease;
                isFastFallingTransition=false;
                isFastFalling=true;
            }
        }
        
        else if(isFastFalling)
        {
            frameVelocity.y+=baseMoveStats.gravityAcc*Time.fixedDeltaTime*baseMoveStats.gravityMultiplierOnJumpRelease;
        }

        else if (!isGrounded)
        {
            frameVelocity.y+=baseMoveStats.gravityAcc*Time.fixedDeltaTime;
        }

        else
        {
            frameVelocity.y=0;
        }

        frameVelocity.y=Math.Clamp(frameVelocity.y,-baseMoveStats.maxVerticalSpeed,baseMoveStats.maxVerticalSpeed);
    }

    private void HandleHorizontalMomentum()
    {
        frameVelocity.x=moveDir.x*moveHorizontalSpeed;
    }

    private bool CheckJumpConditions()
    {
        if(jumpBufferCurrentTimer==baseMoveStats.jumpBufferTimer && isGrounded)
        {
            Debug.Log("Pulo normal");
        }

        if( jumpBufferCurrentTimer>0 && jumpBufferCurrentTimer<baseMoveStats.jumpBufferTimer && isGrounded)
        {
            Debug.Log("Pulo no buffer");
        }
        return jumpBufferCurrentTimer>0f && !isjumping && isGrounded;
    }

    private void InitiateJump()
    {
        isjumping=true;
        jumpBufferCurrentTimer=0f;
        frameVelocity.y=baseMoveStats.jumpInitialSpeed;
    }

    private void CountTimers()
    {
        if (jumpBufferCurrentTimer > 0)
        {
            jumpBufferCurrentTimer=Math.Max(jumpBufferCurrentTimer-Time.deltaTime,0);
        }

        if( currentTimerInApexPoint > 0)
        {
            currentTimerInApexPoint=Math.Max(currentTimerInApexPoint-Time.deltaTime,0);
        }

        if (currentTimerInFastFallingTrasition > 0)
        {
            currentTimerInFastFallingTrasition=Math.Max(currentTimerInFastFallingTrasition-Time.deltaTime,0);
        }
    }

    public void JumpPressed(object sender, EventArgs e)
    {
        jumpBufferCurrentTimer=baseMoveStats.jumpBufferTimer;
        jumpIsHelded=false;
        jumpIsPressed=true;
    }

    public void JumpHelded(object sender, EventArgs e)
    {
        jumpIsHelded=true;
        jumpIsPressed=false;
    }
}
