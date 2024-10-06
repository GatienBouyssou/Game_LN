using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 

    void LateUpdate()
    {
        Vector3 newPosition = player.position + offset;
        Vector3 desiredPosition = new Vector3(newPosition.x, newPosition.y, -20);
        transform.position = desiredPosition;
    }
}
