using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Movement
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 7, -20);

    public void FixedUpdate() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void setTarget(Transform newTarget) {
        target = newTarget;
        transform.position = target.position + offset;
    }

    //fade 
    public float fadeDurationSeconds= 1f;
    public float progress = 0f;
    public Color fadeColor = Color.black;
    public AnimationCurve fadeInCurve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public AnimationCurve fadeOutCurve = new AnimationCurve(new Keyframe(0, 0),
        new Keyframe(0.5f, 0.5f, 1.5f, 1.5f), new Keyframe(1, 1));
    private AnimationCurve curve;

    public void fadeIn(float newfadeDurationSeconds) {
        progress = 0;
        fadeDurationSeconds = newfadeDurationSeconds;
        curve = fadeInCurve;
    }

    public void fadeOut(float newfadeDurationSeconds) {
        progress = 0;
        fadeDurationSeconds = newfadeDurationSeconds;
        curve = fadeOutCurve;
    }

    public void OnGUI() {
        progress += Time.deltaTime / fadeDurationSeconds; // Update progress based on elapsed time
        progress = Mathf.Clamp01(progress); // Ensure progress stays within [0, 1]
        float alpha = curve.Evaluate(progress);
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }

    void Start() {
        fadeIn(1f);
    }
    
}