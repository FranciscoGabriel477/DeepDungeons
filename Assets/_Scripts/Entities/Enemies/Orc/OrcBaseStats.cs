using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName ="Orc Base Moviment Stats")]
public class OrcBaseStats : ScriptableObject
{
    public float moveHorizontalSpeed;
    public float KOtime;
    public float gravityAcc;
    public float rangeOfVision;
    public float attackRange;
    public float attackTime;
    public float decelaration;
    public float patrolRange;
    public float minWardTime;
    public float maxWardTime;
    public float chaseRange;
}
