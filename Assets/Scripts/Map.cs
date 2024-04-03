using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    public bool mapActive = false;

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

    }

}
