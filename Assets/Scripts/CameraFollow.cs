using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = playerTransform.position;
        desiredPosition.y = 0;
        desiredPosition.z = -10;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
