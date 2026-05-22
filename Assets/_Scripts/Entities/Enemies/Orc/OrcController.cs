using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(OrcMover))]
public class OrcController : EntityController
{
    public OrcMover orcMover{get; private set;} 
    public OrcVisual orcVisual{get; private set;}
    public OrcBaseStats baseStats;
    private OrcStateMachine orcStateMachine;
    public PlayerController player;    
    private void Awake()
    {
        orcMover=GetComponent<OrcMover>();
        orcVisual=GetComponentInChildren<OrcVisual>();
        player=FindFirstObjectByType<PlayerController>();
    }

    private void Start()
    {
        moveDir=Vector2.zero;
        IsGrounded=orcMover.CheckGround();;
        isFacingRight=true;
        orcStateMachine=new OrcStateMachine(this);
    }

    private void Update()
    {
        orcStateMachine.Action(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        orcStateMachine.FixedAction(Time.fixedDeltaTime);
        externalForce=Vector2.MoveTowards(externalForce,Vector2.zero,baseStats.decelaration*Time.fixedDeltaTime);
    }

    public void Move()
    {
        orcMover.SetVelocity(frameVelocity+externalForce);
    }

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

}
