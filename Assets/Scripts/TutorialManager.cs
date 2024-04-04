using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image[] popUps;
    public int popupIndex = 0;

    public bool isTutorialActive;
    // Start is called before the first frame update
    void Start()
    {
        isTutorialActive = true;
        Save save = GameObject.Find("save").GetComponent<Save>();
        if (save.GetObject("Tutorial", "")) { popupIndex = 3; }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorialActive)
        {
            // Iterates through the different popups
            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popupIndex)
                {
                    popUps[i].gameObject.SetActive(true);

                }
                else
                {
                    popUps[i].gameObject.SetActive(false);
                }
            }


            // Actions for players to achieve to complete tutorial
            if (popupIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.A) || 
                    Input.GetKeyDown(KeyCode.D ) || 
                    Input.GetKeyDown(KeyCode.W) || 
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.LeftArrow) ||
                    Input.GetKeyDown(KeyCode.RightArrow) ||
                    Input.GetKeyDown(KeyCode.LeftArrow) ||
                    Input.GetKeyDown(KeyCode.UpArrow) ||
                    Input.GetKeyDown(KeyCode.Space))
                {
                    popupIndex++;
                }
            }
            else if (popupIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    popupIndex++;
                }
            }
            else if (popupIndex == 2)
            {
                if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab))
                {
                    popupIndex++;
                    Save save = GameObject.Find("save").GetComponent<Save>();
                    save.SaveObject("Tutorial", "");
                }
            }
            else // Ends Tutorial
            {
                //Debug.Log("Tutorial Out of Index");
            }
        }
    }
        


}
