using UnityEngine;

public class Armadilha : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>().playerStateMachine.GetActualStateName() == PlayerStateName.Death)
        {
            return;
        }
        GameManager.instance.PlayerHittedByTrap();
    }
}
