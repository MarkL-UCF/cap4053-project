using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 0.8f; // Speed of the enemy
    public GameObject projectilePrefab; // Projectile Prefab
    public float fireRate = 1f; // Time between shots
    public float projectileSpeed = 2f; // Speed of the projectile
    private Transform player; // Reference to the player
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.mass = 1000f; 
        
        // Find the player GameObject by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }


    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

}