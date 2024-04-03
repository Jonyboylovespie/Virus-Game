using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    public float maxHealth = 3;
    public float health;
    public GameObject projectilePrefab;
    public float launchForce = 40f;
    public float jumpForce = 35f;
    public float moveSpeed = 15f;
    public LayerMask groundLayer;
    public Sprite[] damagedSprites;
    public GameObject Blood; 
    public Vector2 direction = new Vector2(1, 0);
    
    bool dead;
    float falling = 0; // seconds scince last grounded for animation and coyote time
    float coyoteTime = 0.1f; // seconds after falling that player can still jump
    Rigidbody2D rb;
    Collider2D col;
    Vector3 firePoint;

    // Variables for Animations
    Animator legsAnimator;
    public SpriteRenderer body;
    SpriteRenderer legs;
    SpriteRenderer rightArm;
    SpriteRenderer leftArm;
    Transform bodyTransform;
    Transform legsTransform;
    Transform rightArmTransform;
    Transform leftArmTransform;

    float squash = 0;

    void Start()
    {
        load_save();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        firePoint = transform.Find("FirePoint").localPosition;
        
        // Initialize Variables on start
        legsAnimator = transform.Find("Legs").GetComponent<Animator>();
        body = transform.Find("Body").GetComponent<SpriteRenderer>();
        legs = transform.Find("Legs").GetComponent<SpriteRenderer>();
        rightArm = transform.Find("RightArm").GetComponent<SpriteRenderer>();
        leftArm = transform.Find("LeftArm").GetComponent<SpriteRenderer>();
        bodyTransform = transform.Find("Body");
        legsTransform = transform.Find("Legs");
        rightArmTransform = transform.Find("RightArm");
        leftArmTransform = transform.Find("LeftArm");
        body.sprite = damagedSprites[Mathf.Clamp(Mathf.FloorToInt(health / maxHealth * (damagedSprites.Length - 1)), 0, damagedSprites.Length - 1)];
            
    }
  
    void Update()
    {
        if (dead) { rb.velocity = new Vector2(0f,0f); return; }
        
        falling += Time.deltaTime;
        if (Physics2D.OverlapBox((Vector2)transform.position + col.offset, col.bounds.size, 1f, groundLayer))
        {
            if (falling > coyoteTime) 
            { 
                squash = 0.2f;
            }
            falling = 0;
        }

        if (Input.GetAxisRaw("Horizontal") == 1) { direction = new Vector2(1, 1); } 
        if (Input.GetAxisRaw("Horizontal") == -1) { direction = new Vector2(-1, 1); } 

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y); // Horizontal Movement
        if (Input.GetKeyDown(KeyCode.Mouse0)) { LaunchProjectile(); } // Fire Projectile

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // Vertical Movement
        {
            if (falling < coyoteTime) 
            {
                squash = -0.2f;
                falling = coyoteTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        Animate();
        
    }
    
    void load_save()
    {

        health = maxHealth;
        Save save = GameObject.Find("Save").GetComponent<Save>();
        direction = new Vector2(save.dir, 1);
        if (save.health > 0)
        {
            health = save.health;

            if (save.door != "")
            {
                transform.position = GameObject.Find(save.door).transform.position;
                direction = new Vector2(save.doordir, 1);
                save.door = "";
            }
        } 
        else 
        { 
            if (save.checkpointReached) { transform.position = save.checkpointPosition; }
        }
        GameObject.Find("Camera").GetComponent<CameraFollow>().setTarget(transform);
        
    }

    void Animate() 
    {
        squash *= 0.96f;
        legsAnimator.SetBool("moving", rb.velocity.magnitude > 0);
        legsAnimator.SetBool("jumping", rb.velocity.y > 0);
        legsAnimator.SetBool("grounded", falling == 0);

        //Vector3 bob = new Vector3(0, Mathf.Sin(Time.time * 5f) * 1f, 0);

        //bodyTransform.localPosition = bob;
        bodyTransform.localScale = new Vector3(1 + squash, 1 - squash, 1);
        legsTransform.localScale = new Vector3(1 + squash, 1 + squash, 1);
        rightArmTransform.localScale = new Vector3(1 + squash, 1 + squash, 1);
        leftArmTransform.localScale = new Vector3(1 + squash, 1 + squash, 1);

        body.flipX = direction.x < 0;
        legs.flipX = direction.x < 0;
        rightArm.flipX = direction.x < 0;
        leftArm.flipX = direction.x < 0;
    }

    void LaunchProjectile()
    { 
        GameObject.Find("Camera").GetComponent<CameraFollow>().Shake(.1f, 0.05f);
        Vector3 projectilePosition = transform.position + new Vector3(firePoint.x * direction.x, firePoint.y, firePoint.z);
        GameObject projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        if (projectileRB != null) { projectileRB.AddForce(direction * launchForce, ForceMode2D.Impulse); }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (collision.gameObject.CompareTag("Enemy Projectile"))
        {
            // cameraShake.ShakeCamera();
            health -= projectile.damage;
            Destroy(collision.gameObject);
            if (!dead && health <= 0)
            {
                dead = true;
                GameObject.Find("Camera").GetComponent<CameraFollow>().Shake(.1f, 0.4f);
                StartCoroutine(Respawn());
            } else
            {
                GameObject.Find("Camera").GetComponent<CameraFollow>().Shake(.1f, 0.1f);
                body.sprite = damagedSprites[Mathf.Clamp(Mathf.FloorToInt(health / maxHealth * (damagedSprites.Length - 1)), 0, damagedSprites.Length - 1)];
            }
        }
        
    }

    IEnumerator Respawn()
    {
        
        Vector3 bloodPos = transform.position;
        bloodPos.y += 2;
        Instantiate(Blood, bloodPos, Quaternion.identity);
        
        
        foreach (var spriteRenderer in gameObject.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.enabled = false;
        }
        
        yield return new WaitForSeconds(1f);

        
        dead = false;
   
        Save save = GameObject.Find("Save").GetComponent<Save>();
        save.health = health;
        if (save.checkpointReached) { 
            SceneManager.LoadScene(save.checkpointScene, LoadSceneMode.Single); // load Mode Single makes sure there is only one scene loaded
            } 
        else { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); // reload current scene no checkpoint
            }
       
    }

}