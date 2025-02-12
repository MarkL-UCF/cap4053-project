using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   public float moveSpeed = 0.8f; // Movement speed of the enemy
     private Transform player; // Time interval before changing direction

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Start changing direction periodically
       GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
      if (player != null)
        {
            // Calculate direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Move the enemy towards the player
            rb.velocity = direction * moveSpeed;
        }

    }
}
