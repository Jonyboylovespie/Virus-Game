using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public string destinationScene;
    public float secondsBeforeYouCanSkip = 5f;
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
            Debug.Log("Loading scene: " + destinationScene);
            SceneManager.LoadScene(destinationScene);
        }
    }

    IEnumerator EnableSkip()
    {
        yield return new WaitForSeconds(secondsBeforeYouCanSkip);
        canSkip = true;
    }
}