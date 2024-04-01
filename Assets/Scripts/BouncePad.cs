using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float appliedVelocity = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, appliedVelocity);
        }
    }
}
