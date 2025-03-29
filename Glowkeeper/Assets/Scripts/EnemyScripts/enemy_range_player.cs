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
    private bool isMovingToShadow = false;
    private float targetResetCooldown = 2f;
    private float lastTargetTime = 0f;

    // 🔊 Audio + Flashing
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.2f;
    private bool isDead = false;

    void Start()
    {
        enemyHealth = maxEnemyHealth;
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");

        InvokeRepeating("Shoot", 1f, fireRate);

        // Audio + visuals
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void EnemyDamage(float amount)
    {
        if (isDead) return;

        enemyHealth -= amount;

        // Play hit sound
        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);

        // Flash red
        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        if (enemyHealth <= 0)
        {
            isDead = true;
            StartCoroutine(PlayDeathSoundAndDie());
        }
    }

    IEnumerator PlayDeathSoundAndDie()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
            yield return new WaitForSeconds(deathSound.length);
        }

        Destroy(gameObject);
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
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
                projRb.velocity = shootDirection * projectileSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Light Ring (75%)"))
        {
            if (!isMovingToShadow && Time.time > lastTargetTime + targetResetCooldown)
            {
                targetPosition = GetRandomPointOutsideLight(other.transform.position, avoidLightRadius);
                isMovingToShadow = true;
                lastTargetTime = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        if (isMovingToShadow)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                isMovingToShadow = false;
        }
    }

    private Vector2 GetRandomPointOutsideLight(Vector2 lightPosition, float radius)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(radius, radius * 2);
        return lightPosition + (randomDirection * randomDistance);
    }
}