using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_player_attack : MonoBehaviour
{
    public float enemyHealth;
    public float maxEnemyHealth;
    public float moveSpeed = 0.2f; // Speed of the enemy, adjusted for a slower movement
    public float damageAmount = 0.5f; // Damage per second
    public float damageRate = 1f; // Time between damage ticks
    private Transform player; // Reference to the player
    private float nextDamageTime = 0f; // Timer for damage application

    [SerializeField] Transform target;

    NavMeshAgent agent;

    void Start()
    {
        enemyHealth = maxEnemyHealth;
        // Set up the NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Adjust the agent's speed based on the moveSpeed variable
        agent.speed = moveSpeed;

        // Find the player GameObject by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the flame has the 'Player' tag.");
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

    void Update()
    {
        if (player != null)
        {
            // Set the destination to the flame's position using the NavMeshAgent
            agent.SetDestination(player.position);
        }
    }

    // Continuously damage the flame while touching it
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            playerHealth playerScript = collision.gameObject.GetComponent<playerHealth>();
            if (playerScript != null)
            {
                playerScript.PlayerDamage(damageAmount);
                Debug.Log("Enemy is draining the player's health!");
                nextDamageTime = Time.time + damageRate; // Set the next allowed damage time
            }
        }
    }
}
