using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth;
    public float maxEnemyHealth;
    public float moveSpeed; // Speed of the enemy, adjusted for a slower movement
    public int damageAmount = 10; // Damage per second
    public float damageRate = 1f; // Time between damage ticks
    private Transform flame; // Reference to the player
    private float nextDamageTime = 0f; // Timer for damage application

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
        GameObject flameObj = GameObject.FindGameObjectWithTag("Flame");
        if (flameObj != null)
        {
            flame = flameObj.transform;
        }
        else
        {
            Debug.LogError("Flame not found! Make sure the flame has the 'Flame' tag.");
        }
    }

    void Update()
    {
        if (flame != null)
        {
            // Set the destination to the flame's position using the NavMeshAgent
            agent.SetDestination(flame.position);
        }
    }

    public void EnemyDamage(float amount)
    {
        enemyHealth -= amount;

        //checks if player is dead
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);//destroys player object
        }
    }

    // Continuously damage the flame while touching it
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flame") && Time.time >= nextDamageTime)
        {
            flameHealth flameScript = collision.gameObject.GetComponent<flameHealth>();
            if (flameScript != null)
            {
                flameScript.FlameDamage(damageAmount);
                Debug.Log("Enemy is draining the flame's health!");
                nextDamageTime = Time.time + damageRate; // Set the next allowed damage time
            }
        }
    }
}
