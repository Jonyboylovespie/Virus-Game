using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{

    public bool mapActive = false;
    public string[] markerScenesNames;
    public Sprite[] markerSprites; 

    void Start() {
        updateMap();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab)) { 
            mapActive = !mapActive;
            updateMap(); 
        }
    }

    void updateMap() {

        gameObject.transform.Find("Background")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("Head")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("Torso")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("LeftLeg")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("RightLeg")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("LeftArm")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("RightArm")?.gameObject.SetActive(mapActive);
        gameObject.transform.Find("Marker")?.gameObject.SetActive(mapActive);

        if (!mapActive) return;

        for (int i = 0; i < markerScenesNames.Length; i++) { // Set The Marker To Correct Location
            if (markerScenesNames[i] == SceneManager.GetActiveScene().name) {
                gameObject.transform.Find("Marker").GetComponent<Image>().sprite = markerSprites[i];
                break;
            }
        }
    
    }

}
