using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health;
    public GameObject projectilePrefab;
    public float launchForce = 10f;
    public float jumpForce = 30f;
    public float moveSpeed = 10f;
    public LayerMask groundLayer;
    Vector2 direction = new Vector2(1, 0); 
    Vector3 firePoint;
    Rigidbody2D rb;
    SpriteRenderer body; 
    SpriteRenderer legs;
    SpriteRenderer rightArm;
    SpriteRenderer leftArm;
    public GameObject checkPoints;
    public GameObject projectiles;
    public GameObject Blood;
    public bool dead;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint").localPosition;
        body = transform.Find("Body").GetComponent<SpriteRenderer>();
        legs = transform.Find("Legs").GetComponent<SpriteRenderer>();
        rightArm = transform.Find("RightArm").GetComponent<SpriteRenderer>();
        leftArm = transform.Find("LeftArm").GetComponent<SpriteRenderer>();
    }
  
    void Update()
    {
        if (!dead)
        {
            if (Input.GetAxisRaw("Horizontal") == 1) { direction = new Vector2(1, 1); } 
            if (Input.GetAxisRaw("Horizontal") == -1) { direction = new Vector2(-1, 1); } 

            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y); // Horizontal Movement
            if (Input.GetKeyDown(KeyCode.Mouse0)) { LaunchProjectile(); }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // Vertical Movement
            {
                if (Physics2D.OverlapCircle(transform.position, 1, groundLayer))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            }

            Animate();
        }
    }

    void Animate() 
    {
        body.flipX = direction.x < 0;
        legs.flipX = direction.x < 0;
        rightArm.flipX = direction.x < 0;
        leftArm.flipX = direction.x < 0;
    }

    void LaunchProjectile()
    {
        Vector3 projectilePosition = transform.position + new Vector3(firePoint.x * direction.x, firePoint.y, firePoint.z);
        GameObject projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
        projectile.transform.SetParent(projectiles.transform);
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.origin = "player"; 

        if (projectileRB != null)
        {
            projectileRB.AddForce(direction * launchForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.origin == "player") return;
        if (collision.gameObject.CompareTag("Projectile 1"))
        {
            health -= projectile.damage;
            Destroy(collision.gameObject);
            if (!dead && health <= 0)
            {
                dead = true;
                Respawn();
            }
        }
    }

    void Respawn()
    {
        Vector3 bloodPos = transform.position;
        bloodPos.y += 2;
        Instantiate(Blood, bloodPos, Quaternion.identity);
        foreach (var spriteRenderer in gameObject.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.enabled = false;
        }
        StartCoroutine(WaitForParticles());
    }
    IEnumerator WaitForParticles()
    {
        yield return new WaitForSeconds(1);
        dead = false;
        foreach (var spriteRenderer in gameObject.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.enabled = true;
        }
        int checkPointNumber = checkPoints.GetComponent<CheckPointController>().currentCheckPoint;
        for (int i = 0; i < checkPoints.transform.childCount; i++)
        {
            GameObject childObject = checkPoints.transform.GetChild(i).gameObject;
            if (childObject.name.Contains(checkPointNumber.ToString()))
            {
                transform.position = childObject.transform.position;
                break;
            }
        }
        for (int i = 0; i < projectiles.transform.childCount; i++)
        {
            GameObject childObject = projectiles.transform.GetChild(i).gameObject;
            Destroy(childObject);
        }
        health = 200;
    }
}