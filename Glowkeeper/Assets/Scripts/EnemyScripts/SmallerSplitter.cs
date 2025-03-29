using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallerSplitter : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    private Transform player;
    private NavMeshAgent agent;
    public float enemyHealth = 2f;

    // Audio + Flashing
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.2f;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (!isDead && player != null)
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

    public void SetPlayerTarget(Transform target)
    {
        player = target;
        if (agent != null)
        {
            Debug.Log("New Splitter is now tracking player!");
            agent.SetDestination(player.position);
        }
    }
}