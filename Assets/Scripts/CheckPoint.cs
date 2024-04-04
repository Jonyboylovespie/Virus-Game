using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Hide the sprite when the game starts
        if (spriteRenderer != null) { spriteRenderer.enabled = false; }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "player")
        {
            Save save = GameObject.Find("save").GetComponent<Save>();
            save.checkpointReached = true;
            save.dir = collider.gameObject.GetComponent<PlayerController>().direction.x;
            save.checkpointPosition = transform.position;
            save.checkpointScene = gameObject.scene.name;
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "player")
        {
            Save save = GameObject.Find("save").GetComponent<Save>();
            save.dir = collider.gameObject.GetComponent<PlayerController>().direction.x;
        }
    }

}