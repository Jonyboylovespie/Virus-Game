using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public float yChange = 5;
    public Transform playerTransform;

    private void Start()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 startPos = playerPosition;
        startPos.y = playerPosition.y + yChange;
        startPos.z = -10;
        transform.position = startPos;
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 desiredPosition = playerPosition;
        desiredPosition.y = playerPosition.y + yChange;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = -10;
        transform.position = smoothedPosition;
    }
}
