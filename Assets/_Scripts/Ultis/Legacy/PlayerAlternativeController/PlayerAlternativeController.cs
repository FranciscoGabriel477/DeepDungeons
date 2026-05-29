using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAlternativeController : MonoBehaviour
{
    public PlayerAlternativeMovementStats moveStats;
    //[SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private Collider2D footCollider;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Vector2 moveInput;
    private GameInput gameInput;
    private RaycastHit2D groundHit;
    private RaycastHit2D headHit;
    private int numberOfJumpsUsed;
    public float verticalVelocity{get;private set;}
    private float fastFallReleaseSpeed;
    private float timePastApexThreshold;
    private float jumpBufferTimer;
    private float apexPoint;
    private float coyoteTimer;
    private float fastFallTime;
    private bool isJumping;
    private bool isFalling;
    private bool isFastFalling;
    private bool isGrounded;
    private bool headBumped;
    private bool isFacingRight;
    private bool isRunning;
    private bool isPastApexThreshold;
    private bool jumpReleasedDuringBuffer;
    private bool jumpWasPressed;
    private bool jumpWasHeld;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        isFacingRight=true;
        isRunning=false;
    }
    private void Start()
    {
        gameInput=GameInput.instance;
        //gameInput.OnRunPressed+=RunPressed;
        //gameInput.OnRunHelded+=RunHelded;
        gameInput.OnJumpPressed+=JumpPressed;
        gameInput.OnJumpHelded+=JumpHelded;
    }
    private void Update()
    {
        moveInput=gameInput.GetNormalizedMovementInput();
        //playerVisual.PlayAnimation(moveInput!=Vector2.zero?"Running":"Idle");
        JumpChecks();
        CountTimers();
    }

    

    private void FixedUpdate()
    {
        CollisionsCheck();
        HandleTurn();
        Jump();
        if (isGrounded)
        {
            Move(moveStats.groundAceleration,moveStats.groundDeceleration);
        }
        else
        {
            Move(moveStats.airAceleration,moveStats.airDeceleration);
        }

    }
    private void JumpChecks()
    {
        if (jumpWasPressed) //se o botao de pular é pressionado ativamos o buffer
        {
            jumpBufferTimer=moveStats.jumpBufferTime; // buffer ativo
            jumpReleasedDuringBuffer=false; // é setado como falso pois na verificação a seguir caso o personagem possa pular, ele já vai realizar o salto, ou seja, não precisou do buffer 
        }

        if (jumpWasHeld) // soltou o botão
        {
            if (jumpBufferTimer > 0f) // caso o jogador soltou o botão no meio do buffer
            {
                jumpReleasedDuringBuffer=true; // o jogador ira atingir o chão com o botão solto e vai pulat graças ao buffer
            }
            if (isJumping && verticalVelocity>0f) // verifica se o jogador soltou o botao de pulo durante a subida
            {
                if (isPastApexThreshold) // se o jogador está no apice do pulo
                {
                    isFastFalling=true;
                    isPastApexThreshold=false;
                    fastFallTime=moveStats.timeForUpwardsCancel;
                    verticalVelocity=0f;
                }
                else{
                    isFastFalling=true;
                    fastFallReleaseSpeed=verticalVelocity;
                }
            }
        }

        if (jumpBufferTimer>0f && !isJumping && (isGrounded || coyoteTimer >0f))
        {
            InitiateJump(1);
        }
        else if (jumpBufferTimer>0f && isJumping && numberOfJumpsUsed<moveStats.numberOfJumpsAllowed)
        {
            isFastFalling=false;
            InitiateJump(1);
        }
        else if (jumpBufferTimer>0f && isFalling && numberOfJumpsUsed<moveStats.numberOfJumpsAllowed)
        {
            isFastFalling=false;
            InitiateJump(2);
        }

        if(isGrounded && (isJumping || isFalling) && verticalVelocity<=0){
            isJumping=false;
            isFalling=false;
            isFastFalling=false;
            fastFallTime=0f;
            isPastApexThreshold=false;
            numberOfJumpsUsed=0;
            verticalVelocity=Physics2D.gravity.y;
        }   
    }

    private void InitiateJump(int numberOfJumps)
    {
        if (!isJumping)
        {
            isJumping=true;
        }
        jumpBufferTimer=0f;
        numberOfJumpsUsed+=numberOfJumps;
        verticalVelocity=moveStats.initialJumpVelocity;
    }

    private void Jump()
    {
        if (isJumping)
        {
            if (headBumped)
            {
                isFastFalling=true;
            }

            if (verticalVelocity >= 0f)
            {
                apexPoint=Mathf.InverseLerp(moveStats.initialJumpVelocity,0f,verticalVelocity);
                if(apexPoint > moveStats.apexThresHold)
                {
                    if (!isPastApexThreshold)
                    {
                        isPastApexThreshold=true;
                        timePastApexThreshold=0f;

                    }
                    else
                    {
                        timePastApexThreshold+=Time.fixedDeltaTime;
                        if (timePastApexThreshold < moveStats.apexHangTime)
                        {
                            verticalVelocity=0f;
                        }
                        else
                        {
                            verticalVelocity=-0.01f;
                        }
                    }
                }
                else
                {
                    verticalVelocity+=moveStats.gravity*Time.fixedDeltaTime;
                    if (isPastApexThreshold)
                    {
                        isPastApexThreshold=false;
                    }
                }
            }
            else if (!isFastFalling)
            {
                verticalVelocity+=moveStats.gravity*Time.fixedDeltaTime*moveStats.gravityMultiplierOnJumpRelease;
            }

            else if (verticalVelocity < 0f)
            {
                isFalling=true;
            }
        }

        if (isFastFalling)
        {
            if (fastFallTime >= moveStats.timeForUpwardsCancel)
            {
                verticalVelocity+=moveStats.gravity*Time.fixedDeltaTime*moveStats.gravityMultiplierOnJumpRelease;                
            }
            else if (fastFallTime < moveStats.timeForUpwardsCancel)
            {
                verticalVelocity=Mathf.Lerp(fastFallReleaseSpeed,0f,(fastFallTime/moveStats.timeForUpwardsCancel));
            }
            fastFallTime+=Time.fixedDeltaTime;
        }

        if(!isGrounded && !isJumping)
        {
            if (!isFalling)
            {
                isFalling=true;
            }
            verticalVelocity=moveStats.gravity*Time.fixedDeltaTime;
        }
        verticalVelocity=Mathf.Clamp(verticalVelocity,0f,50);
        rb.linearVelocity= new Vector2(rb.linearVelocityX,verticalVelocity);
    }
    

    private void Move(float aceleration, float deceleration)
    {
        if (moveInput != Vector2.zero)
        {
            Vector2 targerVelocity=Vector2.right*moveInput.x;
            targerVelocity*=isRunning?moveStats.maxRunSpeed : moveStats.maxWalkSpeed;
            moveVelocity=Vector2.Lerp(moveVelocity,targerVelocity,aceleration*Time.fixedDeltaTime);
            rb.linearVelocity=moveVelocity+Vector2.up*rb.linearVelocityY;
        }
        else
        {
            moveVelocity=Vector2.Lerp(moveVelocity,Vector2.zero,deceleration*Time.fixedDeltaTime);
            rb.linearVelocity=moveVelocity+Vector2.up*rb.linearVelocityY;
        }
    }

    private void HandleTurn()
    {

        if (isFacingRight && moveInput.x<0)
        {
            isFacingRight=false;
            transform.Rotate(0,-180f,0);
        }
        else if(!isFacingRight && moveInput.x>0)
        {
            isFacingRight=true;
            transform.Rotate(0,180f,0);
        }
    }
    private void IsGrounded()
    {
        Vector2 boxOrigin= new Vector2(footCollider.bounds.center.x,footCollider.bounds.min.y);
        Vector2 boxSize= new Vector2(footCollider.bounds.size.x,moveStats.groundDetectionRayLenght);
        isGrounded=Physics2D.BoxCast(boxOrigin,boxSize,0,Vector2.down,moveStats.groundDetectionRayLenght,moveStats.groundLayer);
    }
    private void BumpedHead()
    {
        Vector2 boxOrigin= new Vector2(footCollider.bounds.center.x,footCollider.bounds.min.y);
        Vector2 boxSize= new Vector2(footCollider.bounds.size.x*moveStats.headWidth,moveStats.headDetectionRayLenght);
        isGrounded=Physics2D.BoxCast(boxOrigin,boxSize,0,Vector2.up,moveStats.headDetectionRayLenght,moveStats.groundLayer);
    }
    private void RunPressed(object sender, EventArgs e)
    {
        isRunning=true;
    }

    private void RunHelded(object sender, EventArgs e)
    {
        isRunning=false;
    }

    private void CollisionsCheck()
    {
        IsGrounded();
        BumpedHead();
    }

    private void CountTimers()
    {
        jumpBufferTimer-=Time.deltaTime;
        if (!isGrounded)
        {
            coyoteTimer-=Time.deltaTime;
        }
        else
        {
            coyoteTimer=moveStats.jumpCoyoteTime;
        }
    }

    private void JumpPressed(object sender, EventArgs e)
    {
        jumpWasPressed=true;
        jumpWasHeld=false;
    }

    private void JumpHelded(object sender, EventArgs e)
    {
        jumpWasHeld=true;   
        jumpWasPressed=false;   
    }
}
