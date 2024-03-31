using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public string sceneToLoad;

    // Update is called once per frame



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

            SceneManager.LoadScene(sceneToLoad);
            
            Debug.Log("Attempted to load Right arm");
        }
    }

}
