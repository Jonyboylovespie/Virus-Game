using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 3;
    public GameObject projectilePrefab;
    public float cooldownSeconds = 1f;
    public float cooldown = 0f;
    public float launchForce = 20f;
    Vector3 direction = new Vector3(1, 0, 0);
    Vector3 firePoint;
    GameObject player;
    public GameObject Blood;
    Collider2D Range;

    // Variables incicated for damage indicator
    SpriteRenderer enemyBody;
    public Sprite[] damagedSprites;

    void Start()
    {
        Save save = GameObject.Find("Save").GetComponent<Save>(); //disabling for testing
        if (save.GetObject(gameObject.name, gameObject.scene.name)) { Destroy(gameObject); }

        enemyBody = transform.Find("Body").GetComponent<SpriteRenderer>();

        player = GameObject.Find("player");
        firePoint = transform.Find("FirePoint").localPosition;
        Range = transform.Find("Range").GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Damages player when contacted by enemy projectile
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(collision.gameObject); // Destroy the projectile on contact with the enemy
            health -= 1; // Reduce health by a fixed amount (adjust as needed)

            if (health <= 0)
            {
                Vector3 bloodPos = transform.position;
                bloodPos.y += 2;
                Instantiate(Blood, bloodPos, Quaternion.identity);
                GameObject.Find("Camera").GetComponent<CameraFollow>().Shake(.1f, 0.3f);

                Save save = GameObject.Find("Save").GetComponent<Save>(); //disabling for testing 
                save.SaveObject(gameObject.name, gameObject.scene.name);
                
                GameObject enemies = GameObject.Find("Enemies");
                if (enemies == null) { save.SaveObject("", gameObject.scene.name); } // save scene if no enemies are left
                if (enemies.transform.childCount == 1) { save.SaveObject("", gameObject.scene.name); } // save scene if no enemies are left

                Destroy(gameObject);
            }else
            {
               enemyBody.sprite = damagedSprites[Mathf.Clamp(Mathf.FloorToInt(health / 3 * (damagedSprites.Length - 1)), 0, damagedSprites.Length - 1)];
                
            }
        }
    }

    private void Update()
    {
        Animate();

        if(player == null) { return; }
        if (!Range.OverlapPoint(player.transform.position)) return;
        if (player.transform.position.x < transform.position.x) { direction = new Vector3(-1, 0, 0);} 
        else { direction = new Vector3(1, 0, 0); } // Set direction based on player position
        if (cooldown > 0) { cooldown -= Time.deltaTime; return; }
        LaunchProjectile();
        cooldown = cooldownSeconds;
        
    }
    
    void LaunchProjectile()
    {
        GameObject.Find("Camera").GetComponent<CameraFollow>().Shake(.1f, 0.03f);

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
        SpriteRenderer body = transform.Find("Body").GetComponent<SpriteRenderer>();
        SpriteRenderer legs = transform.Find("Legs").GetComponent<SpriteRenderer>();
        SpriteRenderer face = transform.Find("Face").GetComponent<SpriteRenderer>();
        body.flipX = direction.x < 0;
        legs.flipX = direction.x < 0;
        face.flipX = direction.x < 0;
    }
}
