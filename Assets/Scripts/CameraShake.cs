using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1.0f;

    private Vector3 initialPosition;


    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition += Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
        }
    }

    public void ShakeCamera()
    {
        shakeDuration = 0.1f;
    }
}
