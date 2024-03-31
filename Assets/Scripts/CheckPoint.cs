using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) { spriteRenderer.enabled = false; }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "player")
        {
            Save save = GameObject.Find("Save").GetComponent<Save>();
            save.checkpointReached = true;
            save.checkpointPosition = transform.position;
            save.checkpointScene = gameObject.scene.name;
        }
    }
}