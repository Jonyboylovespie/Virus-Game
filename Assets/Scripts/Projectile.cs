using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController player;
    private Vector2 direction;
    public float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
        SpriteRenderer projectileSprite = GetComponent<SpriteRenderer>();

        projectileSprite.flipX = rb.velocity.x < 0;

    }

}
