using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public string destinationScene;
    public float secondsBeforeYouCanSkip = 5f;
    public GameObject camera;
    bool canSkip = false;

    void Start()
    {
        StartCoroutine(EnableSkip());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!canSkip) { return; }
            if (string.IsNullOrEmpty(destinationScene)) { return; }
            StartCoroutine(Continue());
            canSkip = false;
        }
    }

    IEnumerator Continue()
    {
        camera.GetComponent<CameraFollow>().fadeOut(1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(destinationScene);
    }

    IEnumerator EnableSkip()
    {
        yield return new WaitForSeconds(secondsBeforeYouCanSkip);
        canSkip = true;
    }
}