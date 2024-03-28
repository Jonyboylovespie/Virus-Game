using UnityEngine;

public class Wall : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile 1"))
        {
            Destroy(collision.gameObject);
        }
    }
}
