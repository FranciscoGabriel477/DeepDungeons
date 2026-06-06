using UnityEngine;

public class KeepVisualRotation : MonoBehaviour
{
    Quaternion initialRotation;
    void Start()
    {
        initialRotation=transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation=initialRotation;
    }
}
