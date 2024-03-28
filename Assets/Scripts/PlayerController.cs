using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject leftFirePoint;
    public GameObject rightFirePoint;
    public float launchForce = 10f;
    public float jumpForce = 10f;

    public Transform currentFirePoint;
    public float moveSpeed = 10f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFirePoint = rightFirePoint.transform; // Start with the right fire point
    }
  
    

    void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal"); // Assuming character moves on the X-axis
        

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump Controller
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("Jump Pressed");
        }

        // Switches which side the projectile comes from. Also toggles the left and right fire points
        if (moveDirection < 0)
        {
            ChangeCurrentFirePoint(0);

        }
        else if (moveDirection > 0)
        {
            ChangeCurrentFirePoint(1);
        }

        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your desired input button
        {
            LaunchProjectile();
        }
    }

    void ChangeCurrentFirePoint(int firePointSide)
    {
        // fire point side 0 = left, 1 = right
        if (firePointSide == 0)
        {
            currentFirePoint = leftFirePoint.transform;
            rightFirePoint.SetActive(false);
            leftFirePoint.SetActive(true);
        }else if (firePointSide == 1)
        {
            currentFirePoint = rightFirePoint.transform;
            leftFirePoint.SetActive(false);
            rightFirePoint.SetActive(true);
        }
        else
        {
            Debug.Log("Invalid Input on function (ChangeCurrentFirePoint)");
        }
    }


    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, currentFirePoint.position, currentFirePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(currentFirePoint.forward * launchForce, ForceMode.Impulse);
        }
    }
}