using UnityEngine;

public class Trap : MonoBehaviour
{
    // Điểm di chuyển A và B, kéo thả trong Inspector
    public Transform pointA;
    public Transform pointB;

    // Tốc độ di chuyển
    public float speed = 2f;

    // Vị trí mục tiêu hiện tại
    private Vector3 targetPosition;

    void Start()
    {
        // Khởi đầu di chuyển về điểm B
        if (pointB != null)
            targetPosition = pointB.position;
        else
            Debug.LogError("Trap: pointB chưa được gán!");
    }

    void Update()
    {
        if (pointA == null || pointB == null)
            return; // Nếu chưa có điểm thì không chạy

        // Di chuyển từ vị trí hiện tại đến targetPosition
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Nếu gần đến targetPosition thì đổi hướng di chuyển
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (targetPosition == pointB.position)
                targetPosition = pointA.position;
            else
                targetPosition = pointB.position;
        }
    }
}
