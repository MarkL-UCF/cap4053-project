using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Boolean isShadow;
    public float enemyHealth;
    public float moveSpeed = 0.8f; // Speed of the enemy
    public GameObject projectilePrefab; // Projectile Prefab
    public float fireRate = 1f; // Time between shots
    public float projectileSpeed = 2f; // Speed of the projectile
    private Transform flame; // Reference to the player
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.mass = 1000f; 
        
        // Find the player GameObject by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Flame");
        if (playerObj != null)
        {
            flame = playerObj.transform;
        }
        else
        {
            Debug.LogError("Flame not found! Make sure the player has the 'Player' tag.");
        }


    }
    public void EnemyDamage(int amount)
    {
        enemyHealth -= amount;

        //checks if player is dead
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);//destroys player object
        }
    }

    void FixedUpdate()
    {
        if (flame != null)
        {
            Vector2 direction = (flame.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

}