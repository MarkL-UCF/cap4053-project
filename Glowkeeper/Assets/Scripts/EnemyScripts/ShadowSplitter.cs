using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowSplitter : MonoBehaviour
{
    public GameObject smallerSplitterPrefab;
    public float enemyHealth;
    public float maxEnemyHealth = 4f;

    private bool hasSplit = false;
    private Transform player;
    private NavMeshAgent agent;

    // Audio + Flashing
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.2f;

    void Start()
    {
        enemyHealth = maxEnemyHealth;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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
        if (player != null && agent != null)
        {
            agent.SetDestination(player.position);
        }
    }

    public void EnemyDamage(float amount)
    {
        if (enemyHealth <= 0 || hasSplit) return;

        enemyHealth -= amount;

        // Hit sound
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Flash red
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (enemyHealth <= 0 && !hasSplit)
        {
            SplitIntoSmallerVersions();
        }
    }

    void SplitIntoSmallerVersions()
    {
        hasSplit = true;

        Debug.Log("Shadow Splitter is splitting!");

        // Death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Spawn two smaller versions
        Instantiate(smallerSplitterPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(smallerSplitterPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);

        Destroy(gameObject, deathSound != null ? deathSound.length : 0f);
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}