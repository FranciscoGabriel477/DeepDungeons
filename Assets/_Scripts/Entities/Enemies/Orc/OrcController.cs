using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(OrcMover))]
public class OrcController : EntityController
{
    public OrcMover orcMover{get; private set;} 
    public OrcVisual orcVisual{get; private set;}
    public OrcBaseStats baseStats;
    public OrcWeapon orcWeapon;
    private OrcStateMachine orcStateMachine;
    public PlayerController player;    
    public Vector3 wardPosition;
    private void Awake()
    {
        orcMover=GetComponent<OrcMover>();
        orcVisual=GetComponentInChildren<OrcVisual>();
        orcWeapon=GetComponentInChildren<OrcWeapon>();
        player=FindFirstObjectByType<PlayerController>();
        wardPosition=transform.position;
    }

    private void Start()
    {
        moveDir=Vector2.zero;
        IsGrounded=orcMover.CheckGround();;
        isFacingRight=true;
        orcStateMachine=new OrcStateMachine(this);
        orcStateMachine.StateChanged+=orcVisual.MainStateChanged;
    }

    private void Update()
    {
        orcStateMachine.Action(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        orcStateMachine.FixedAction(Time.fixedDeltaTime);
        //Debug.Log(orcStateMachine.GetActualStateName());
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,baseStats.decelaration*Time.fixedDeltaTime);
        HandleRotation();
        Move();
    }

    public void Move()
    {
        orcMover.SetVelocity(frameVelocity+externalForce);
    }

    protected virtual void HandleRotation()
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

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    public override void GetHit(float damage,Vector2 knockBack)
    {
        base.GetHit(damage,knockBack);
        orcStateMachine.SwitchState("Hurt");
    }
}
