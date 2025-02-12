using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float lifetime = 3f; // Destroy after 5 seconds

    void Start()
    {
        // Destroy projectile after a certain time to prevent memory issues
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hits the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            // TODO: Implement player damage logic
            Destroy(gameObject); // Destroy the projectile on impact
        }
       
    }
}