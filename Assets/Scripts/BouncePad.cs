using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float appliedVelocity = 50;
    public AudioSource bounceSound; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bounceSound.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            bounceSound.Play();
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, appliedVelocity);
        }
    }
}
