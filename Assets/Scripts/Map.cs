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
        Save save = GameObject.Find("save").GetComponent<Save>(); 
        if (save == null) { return; }
        if (!save.GetObject("", "Head")) { return; }
        if (!save.GetObject("", "Torso")) { return; }
        if (!save.GetObject("", "Right Arm")) { return; }
        if (!save.GetObject("", "Left Arm")) { return; }
        if (!save.GetObject("", "Right Leg")) { return; }
        if (!save.GetObject("", "Left Leg")) { return; }

        SceneManager.LoadScene("Final CutScene", LoadSceneMode.Single);

    }

    void updateCleared() {
        Save save = GameObject.Find("save").GetComponent<Save>(); //disabling for testing 
        if (save == null) { return; }

        //save.SaveObject("", "Right Arm");// Comment out... just for testing
        //save.SaveObject("", "Head"); 
        //save.SaveObject("", "Torso");
        //save.SaveObject("", "Left Arm");
        //save.SaveObject("", "Right Leg");
        //save.SaveObject("", "Left Leg");

        //Debug.Log("test"); // Comment out... just for testing        
        gameObject.transform.Find("Head")?.gameObject.SetActive(!save.GetObject("", "Head") && mapActive);
        gameObject.transform.Find("Torso")?.gameObject.SetActive(!save.GetObject("", "Torso") && mapActive);
        gameObject.transform.Find("RightArm")?.gameObject.SetActive(!save.GetObject("", "Right Arm") && mapActive);
        gameObject.transform.Find("LeftArm")?.gameObject.SetActive(!save.GetObject("", "Left Arm") && mapActive);
        gameObject.transform.Find("RightLeg")?.gameObject.SetActive(!save.GetObject("", "Right Leg") && mapActive);
        gameObject.transform.Find("LeftLeg")?.gameObject.SetActive(!save.GetObject("", "Left Leg") && mapActive);

    }

    void updateMap() {

        updateCleared();
        gameObject.transform.Find("Background")?.gameObject.SetActive(mapActive);
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
