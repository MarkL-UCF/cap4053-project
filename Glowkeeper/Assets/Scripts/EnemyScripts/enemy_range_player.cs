using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_range_player : MonoBehaviour
{
    public float enemyHealth;
    public float maxEnemyHealth = 5f;
    public GameObject projectilePrefab;
    public float fireRate = 2f;
    public float projectileSpeed = 3f;
    private Transform player;
    private Rigidbody2D rb;

    public float avoidLightRadius = 3f; 
    public float moveSpeed = 1.5f;
    private Vector2 targetPosition; 
    private bool isMovingToShadow = false; // 🔥 New flag to avoid constant resetting
    private float targetResetCooldown = 2f; // 🔥 Timer to prevent instant resets
    private float lastTargetTime = 0f;

    void Start()
    {
        enemyHealth = maxEnemyHealth;
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        InvokeRepeating("Shoot", 1f, fireRate);
    }

    public void EnemyDamage(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (player != null)
        {
            Vector2 shootDirection = (player.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + new Vector3(shootDirection.x * 0.5f, shootDirection.y * 0.5f, 0);
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

            if (projRb != null)
            {
                projRb.velocity = shootDirection * projectileSpeed;
            }
        }
    }

    // 🔥 **Detect Light and Move to Shadows with Delay**
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Light Ring (75%)"))
        {
            Debug.Log("Enemy is in light! Trying to move...");

            if (!isMovingToShadow && Time.time > lastTargetTime + targetResetCooldown)
            {
                Debug.Log("Enemy is now moving to shadows!");
                targetPosition = GetRandomPointOutsideLight(other.transform.position, avoidLightRadius);
                isMovingToShadow = true; // Mark that movement has started
                lastTargetTime = Time.time; // Reset cooldown
            }
        }
    }

    void FixedUpdate()
    {
        if (isMovingToShadow)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            // 🔥 Stop moving if close enough to target
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMovingToShadow = false; // Stop movement
            }
        }
    }

    // **Finds a random spot outside the light ring**
    private Vector2 GetRandomPointOutsideLight(Vector2 lightPosition, float radius)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(radius, radius * 2);
        return lightPosition + (randomDirection * randomDistance);
    }
}