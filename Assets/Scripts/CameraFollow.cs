using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        transform.position = playerTransform.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = playerTransform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y = 0;
        smoothedPosition.z = -10;
        transform.position = smoothedPosition;
    }
}
