using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth;
    public float maxEnemyHealth;
    public float moveSpeed;
    public int damageAmount = 10;
    public float damageRate = 1f;

    private Transform flame;
    private float nextDamageTime = 0f;

    private NavMeshAgent agent;

    // Audio + Flashing
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.2f;


    private bool isDead = false;

    private Transform player;

    void Start()
    {
        enemyHealth = maxEnemyHealth;

        // NavMesh setup
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        // Target flame
        GameObject flameObj = GameObject.FindGameObjectWithTag("Flame");
        if (flameObj != null)
        {
            flame = flameObj.transform;
        }
        else
        {
            Debug.LogError("Flame not found! Make sure the flame has the 'Flame' tag.");
        }


        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }


        // Audio + visuals
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (isDead) return;

        if (PauseController.IsGamePaused)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = moveSpeed;
        }

            if (flame != null)
        {
            agent.SetDestination(flame.position);
        }
        else if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    public void EnemyDamage(float amount)
    {

        if (isDead) return; 

        enemyHealth -= amount;


        // Play hit sound
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Flash red
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (enemyHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            StartCoroutine(PlayDeathSoundAndDie());
        }
    }

    IEnumerator PlayDeathSoundAndDie()
{
    if (audioSource != null && deathSound != null)
    {
        audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(deathSound.length); // wait before destroying
    }

    Destroy(gameObject);
}

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flame") && Time.time >= nextDamageTime)
        {
            flameHealth flameScript = collision.gameObject.GetComponent<flameHealth>();
            if (flameScript != null && !PauseController.IsGamePaused)
            {
                flameScript.FlameDamage(damageAmount);
                Debug.Log("Enemy is draining the flame's health!");
                nextDamageTime = Time.time + damageRate;
            }
        }

        else if (collision.gameObject.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            playerHealth playerScript = collision.gameObject.GetComponent<playerHealth>();
            if (playerScript != null && !PauseController.IsGamePaused)
            {
                playerScript.PlayerDamage(damageAmount);
                Debug.Log("Enemy is draining the player's health!");
                nextDamageTime = Time.time + damageRate; // Set the next allowed damage time
            }
        }
    }
}