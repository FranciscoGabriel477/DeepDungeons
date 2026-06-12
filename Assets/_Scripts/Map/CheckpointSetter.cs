using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().checkPoint=transform.position;
    }
}
