using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController player;
    private Vector2 direction;
    public float damage;
    public GameObject origin = null;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        // Checks which fire point is active and sends the projectile flying in that direction
        if (player.rightFirePoint.activeSelf)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        rb.velocity = direction.normalized * player.launchForce;
    }
}
