using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public Projectile projectile;
    public GameObject projectilePrefab;
    public float launchForce = 50f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        projectile = collision.gameObject.GetComponent<Projectile>();
        if (collision.gameObject.CompareTag("Projectile 1"))
        {
            health -= projectile.damage; // Subtracts Health from enemy
            Destroy(collision.gameObject); // Destorys projectile on contact with enemy
            
            // If enemy health below or equal to 0, destory enemy
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        // GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Physics2D.IgnoreCollision(transform.GetComponent<BoxCollider2D>(), projectile.GetComponent<BoxCollider2D>(), true);
        if (rb != null)
        {
            rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        }
    }
}
