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
        if (Input.GetKeyDown(KeyCode.Space) 
        || Input.GetKeyDown(KeyCode.Return) 
        || Input.GetKeyDown(KeyCode.KeypadEnter)
        || Input.GetKeyDown(KeyCode.Mouse0))
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