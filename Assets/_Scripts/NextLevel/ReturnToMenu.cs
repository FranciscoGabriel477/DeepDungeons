using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController a))
        {
            GameManager.instance.PlayAgain();
            Destroy(gameObject);
        }
    }
}
