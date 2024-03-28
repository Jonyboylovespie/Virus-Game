using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variable initialization
    public float health;
    public Projectile projectile;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        projectile = collision.gameObject.GetComponent<Projectile>();
        // Debug.Log("Collisions Recognized");
        if (collision.gameObject.CompareTag("Projectile 1"))
        {
            // Debug.Log("Collisions is a Prjectile 1"); Debug purposes

            
            health -= projectile.damage; // Subtracts Health from enemy
            Destroy(collision.gameObject); // Destorys projectile on contact with enemy
            
            // If enemy health below or equal to 0, destory enemy
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
