using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public Projectile projectile;
    public Rigidbody2D rigidBody;

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
}
