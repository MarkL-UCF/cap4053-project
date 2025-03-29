using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float enemyHealth;
    public float maxEnemyHealth = 5f;
    public float moveSpeed = 0.8f;
    public GameObject projectilePrefab;
    public float fireRate = 2f;
    public float projectileSpeed = 3f;
    private Transform flame;
    private Rigidbody2D rb;

    public float avoidLightRadius = 3f;
    private Vector2 targetPosition;
    private bool isMovingToShadow = false;
    private bool inLight = false;

    // 🔊 Audio + Flashing
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.2f;

    private bool isDead = false;

    //light when hit/dies
    public GameObject hitLightObject; // Assign the light in the inspector
    public float lightDuration = 0.2f;


    private Transform player;

    void Start()
    {
        enemyHealth = maxEnemyHealth;
        rb = GetComponent<Rigidbody2D>();

        GameObject Flame = GameObject.FindGameObjectWithTag("Flame");
        if (Flame != null)
        {
            flame = Flame.transform;
        }
        else
        {
            Debug.LogError("Flame not found! Make sure the player has the 'Flame' tag.");
        }

         GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            player = Player.transform;
        }

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

        //  Play hit sound
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        //  Flash red
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (hitLightObject != null)
        {
            StartCoroutine(FlashLight());
            StartCoroutine(FlashLight());
        }

        if (enemyHealth <= 0)
        {
            isDead = true;
            StartCoroutine(PlayDeathSoundAndDie());
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
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

    IEnumerator FlashLight()
    {
        hitLightObject.SetActive(true);
        yield return new WaitForSeconds(lightDuration);
        hitLightObject.SetActive(false);
    }
    void Shoot()
    {

        Transform target = flame != null ? flame : player;

        if (target != null)
        {
            Vector2 shootDirection = (target.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + new Vector3(shootDirection.x * 0.5f, shootDirection.y * 0.5f, 0);
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

            if (projRb != null)
            {
                projRb.velocity = shootDirection * projectileSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Light Ring (75%)") || other.CompareTag("Light Ring (50%)") || other.CompareTag("Light Ring (25%)")) && !isMovingToShadow)
        {
            Debug.Log("Enemy2 entered light! Finding shadow...");

            targetPosition = GetShadowPosition(other.transform.position);
            isMovingToShadow = true;
            inLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light Ring (75%)") || other.CompareTag("Light Ring (50%)") || other.CompareTag("Light Ring (25%)"))
        {
            Debug.Log("Enemy2 exited light! Stopping movement.");
            isMovingToShadow = false;
            inLight = false;
        }
    }

    void FixedUpdate()
    {
        if (isMovingToShadow)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                Debug.Log("Enemy2 reached shadow. Stopping movement.");
                isMovingToShadow = false;
            }
        }
    }

    private Vector2 GetShadowPosition(Vector2 lightPosition)
    {
        Vector2 directionAwayFromLight = (transform.position - (Vector3)lightPosition).normalized;
        float randomDistance = Random.Range(avoidLightRadius, avoidLightRadius * 2);
        return (Vector2)transform.position + (directionAwayFromLight * randomDistance);
    }
}