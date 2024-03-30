using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 3;
    public GameObject projectilePrefab;
    public float cooldownSeconds = 1f;
    public float cooldown = 0f;
    public float launchForce = 10f;
    Vector3 direction = new Vector3(1, 0, 0);
    Vector3 firePoint;
    GameObject player;
    SpriteRenderer body;
    SpriteRenderer legs;
    SpriteRenderer face;
    public GameObject Blood;

    void Start()
    {
        player = GameObject.Find("player");
        firePoint = transform.Find("FirePoint").localPosition;
        body = transform.Find("Body").GetComponent<SpriteRenderer>();
        legs = transform.Find("Legs").GetComponent<SpriteRenderer>();
        face = transform.Find("Face").GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (collision.gameObject.CompareTag("player projectile"))
        {
            Destroy(collision.gameObject); // Destroy the projectile on contact with the enemy
            health -= 1; // Reduce health by a fixed amount (adjust as needed)

            if (health <= 0)
            {
                Vector3 bloodPos = transform.position;
                bloodPos.y += 2;
                Instantiate(Blood, bloodPos, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }


    private void Update()
    {
        if (player.transform.position.x < transform.position.x) { direction = new Vector3(-1, 0, 0);} else { direction = new Vector3(1, 0, 0); } // Face the player

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            LaunchProjectile();
            cooldown = cooldownSeconds;
        }
        Animate();
    }

    void LaunchProjectile()
    {

        Vector3 projectilePosition = transform.position + new Vector3(firePoint.x * direction.x, firePoint.y, firePoint.z);
        GameObject projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
    
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();

        // Apply force to the projectile
        if (projectileRB != null)
        {
            projectileRB.AddForce(direction * launchForce, ForceMode2D.Impulse);
        }
    }

    void Animate()
    {
        body.flipX = direction.x < 0;
        legs.flipX = direction.x < 0;
        face.flipX = direction.x < 0;
    }
}
