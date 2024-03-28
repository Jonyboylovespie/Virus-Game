using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController player;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();


        
        if (player.rightFirePoint.activeSelf)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        rb.velocity = direction.normalized * player.launchForce;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
