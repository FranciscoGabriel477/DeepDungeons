using UnityEngine;

public class Armadilha : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Armadilha");

    }
}
