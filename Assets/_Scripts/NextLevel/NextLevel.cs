using UnityEngine;

public class NextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entrando " + collision);
        if(collision.TryGetComponent<PlayerController>(out PlayerController a))
        {
            GameManager.instance.PassLevel();
            Destroy(gameObject);
        }
    }
}
