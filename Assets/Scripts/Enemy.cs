using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 3;
    public GameObject projectilePrefab;
    public float cooldownSeconds = 1f;
    public float cooldown = 0f;
    public float launchForce = 20f;
    Vector3 direction = new Vector3(1, 0, 0);
    Vector3 firePoint;
    Vector3 firePointTop;
    Vector3 firePointBottom;
    GameObject player;
    public GameObject Blood;
    Collider2D Range;

    public AudioSource shootSound;
    public AudioSource hurtSound;
    public AudioSource deathSound;

    public bool isEnemyTall;
    public bool isFirePointBottom;

    // Variables incicated for damage indicator
    SpriteRenderer enemyBody;
    public Sprite[] damagedSprites;

    void Start()
    {
        if (gameObject.CompareTag("Enemy Short"))
        {
            firePoint = transform.Find("FirePoint").localPosition;
            isEnemyTall = false;
        } else if (gameObject.CompareTag("Enemy Tall"))
        {
            firePointTop = transform.Find("FirePoint Top").localPosition;
            firePointBottom = transform.Find("FirePoint Bottom").localPosition;
            isEnemyTall = true;
        }
        
        Save save = GameObject.Find("save").GetComponent<Save>(); //disabling for testing
        if (save.GetObject(gameObject.name, gameObject.scene.name)) { Destroy(gameObject); }

        enemyBody = transform.Find("Body").GetComponent<SpriteRenderer>();

        player = GameObject.Find("player");
        
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
            if (health > 0) { hurtSound.Play();}

            if (health <= 0)
            {
                Vector3 bloodPos = transform.position;
                bloodPos.y += 2;
                Instantiate(Blood, bloodPos, Quaternion.identity);
                GameObject.Find("camera").GetComponent<CameraFollow>().Shake(.1f, 0.3f);

                Save save = GameObject.Find("save").GetComponent<Save>(); //disabling for testing 
                save.SaveObject(gameObject.name, gameObject.scene.name);

                GetComponent<Collider2D>().enabled = false;
                SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer renderer in spriteRenderers) {
                    renderer.enabled = false;
                }

                StartCoroutine(death());
                
            }else
            {
               enemyBody.sprite = damagedSprites[Mathf.Clamp(Mathf.FloorToInt(health / 3 * (damagedSprites.Length - 1)), 0, damagedSprites.Length - 1)];
                
            }
        }
    }

    IEnumerator death() {
        deathSound.Play();

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    

    private void Update()
    {
        Animate();

        if(player == null) { return; }
        if (!Range.OverlapPoint(player.transform.position)) return;
        if (player.transform.position.x < transform.position.x) { direction = new Vector3(-1, 0, 0);} 
        else { direction = new Vector3(1, 0, 0); } // Set direction based on player position
        if (cooldown > 0) { cooldown -= Time.deltaTime; return; }
        if (health <= 0) { return; }
        LaunchProjectile();
        cooldown = cooldownSeconds;
        
    }
    
    void LaunchProjectile()
    {
        GameObject projectile;
        Rigidbody2D projectileRB;
        Vector3 projectilePosition;
        if (isEnemyTall)
        {
            if (isFirePointBottom == true)
            {
                projectilePosition = transform.position + new Vector3(firePointBottom.x * direction.x, firePointBottom.y, firePointBottom.z);
                projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
                shootSound.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                shootSound.Play();
                projectileRB = projectile.GetComponent<Rigidbody2D>();
                
                isFirePointBottom = false;
            }
            else
            {
                projectilePosition = transform.position + new Vector3(firePointTop.x * direction.x, firePointTop.y, firePointTop.z);
                projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
                shootSound.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                shootSound.Play();
                projectileRB = projectile.GetComponent<Rigidbody2D>();
            
                isFirePointBottom = true;
            }
        }
        else
        {
            GameObject.Find("camera").GetComponent<CameraFollow>().Shake(.1f, 0.03f);

            projectilePosition = transform.position + new Vector3(firePoint.x * direction.x, firePoint.y, firePoint.z);
            projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
            shootSound.Play();
            projectileRB = projectile.GetComponent<Rigidbody2D>();
    
            projectileRB = projectile.GetComponent<Rigidbody2D>();

            // Apply force to the projectile
            
        }
        
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
