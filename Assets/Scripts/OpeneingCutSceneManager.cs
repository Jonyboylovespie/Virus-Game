using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OpeneingCutSceneManager : MonoBehaviour
{

    public float timeOfOpening = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTransition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(timeOfOpening);
        //SceneManager.UnloadSceneAsync("Opening CutScene");
        SceneManager.LoadScene("Right Arm", LoadSceneMode.Single);
    }
}
