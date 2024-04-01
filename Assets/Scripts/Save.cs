using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// This script is used to save the player's save data between scenes
// Attach only to default open scene 

public class Save : MonoBehaviour
{
    private static Save instance;
    void Awake() {
        transform.SetParent(null, true);
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        // if (SceneManager.sceneCount > 1) { SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); }
    }

    public void SaveObject(string objectName, string sceneName)
    {
        string key = $"{sceneName}_{objectName}";
        if (!collectedObjects.ContainsKey(key))
        {
            collectedObjects.Add(key, true);
        }
    }

    public bool GetObject(string objectName, string sceneName)
    {
        string key = $"{sceneName}_{objectName}";
        return collectedObjects.ContainsKey(key);
    }

    public float health = 3;
    public bool checkpointReached = false;
    public string checkpointScene = "";
    public string door = "";
    public float doordir = 1;
    public float dir = 1;
    public Vector3 checkpointPosition = new Vector3(0, 0, 0);
    public Dictionary<string, bool> collectedObjects = new Dictionary<string, bool>();

}