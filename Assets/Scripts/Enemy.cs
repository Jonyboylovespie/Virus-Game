using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public GameObject projectilePrefab;
    public float cooldownSeconds = 0.2f;
    public float launchForce = 50f;
    
    float cooldown = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.origin == gameObject)
        { 
            // Prevent the projectile from hitting the one that launched it
            return;
        }

        if (collision.gameObject.CompareTag("Projectile 1"))
        {
            Destroy(collision.gameObject); // Destroy the projectile on contact with the enemy
            health -= 10; // Reduce health by a fixed amount (adjust as needed)

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.origin = gameObject; // Set the origin to the current enemy
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Physics2D.IgnoreCollision(transform.GetComponent<BoxCollider2D>(), projectile.GetComponent<BoxCollider2D>(), true);
        if (rb != null)
        {
            rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            LaunchProjectile();
            cooldown = cooldownSeconds;
        }
    }
}
