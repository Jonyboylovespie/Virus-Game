using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitioner : MonoBehaviour
{
    public string destinationScene;
    public string destinationDoor;
    public int doordir = 1;
    public bool hasExited = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        hasExited = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasExited = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(Travel(collision)); 
        }

    IEnumerator Travel(Collider2D collision) {
        if (!hasExited) { yield break;; }
        if (!collision.gameObject.CompareTag("Player")) { yield break;}

        GameObject.Find("Camera").GetComponent<CameraFollow>().fadeOut(.5f);
        yield return new WaitForSeconds(.5f);

        Save save = GameObject.Find("Save").GetComponent<Save>();
        save.door = destinationDoor;
        save.doordir = collision.gameObject.GetComponent<PlayerController>().direction.x;
        save.health = collision.gameObject.GetComponent<PlayerController>().health;
        SceneManager.LoadScene(destinationScene, LoadSceneMode.Single);
        yield return null;
    }
}