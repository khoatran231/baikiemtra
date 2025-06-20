using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Kéo thả player vào inspector
    public Vector3 offset = new Vector3(0, 0, -10); // Khoảng cách camera so với player

    void LateUpdate()
    {
        if (player != null)
            transform.position = player.position + offset;
    }
}
