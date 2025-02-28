using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_range_player : MonoBehaviour
{
    public float enemyHealth;
    public float maxEnemyHealth;
    public GameObject projectilePrefab; // Projectile Prefab
    public float fireRate = 2f; // Time between shots
    public float projectileSpeed = 3f; // Speed of the projectile
    private Transform flame; // Reference to the player

    void Start()
    {
        enemyHealth = maxEnemyHealth;
        // Find the player GameObject by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            flame = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // Start shooting projectiles at intervals
        InvokeRepeating("Shoot", 1f, fireRate);
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

    void Shoot()
    {
        if (flame != null)
        {
            // Calculate direction towards the player
            Vector2 shootDirection = (flame.position - transform.position).normalized;

            // Spawn position slightly in front of the enemy
            Vector3 spawnPosition = transform.position + new Vector3(shootDirection.x * 0.5f, shootDirection.y * 0.5f, 0);

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

            // Get Rigidbody2D of the projectile
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

            if (projRb != null)
            {
                // Apply velocity to the projectile
                projRb.velocity = shootDirection * projectileSpeed;
            }
        }
    }
}