using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Boolean isShadow;
    public float enemyHealth;
    public float moveSpeed = 0.8f; // Speed of the enemy
    public int damageAmount = 10; // Damage per second
    public float damageRate = 1f; // Time between damage ticks
    private Transform flame; // Reference to the player
    private Rigidbody2D rb;
    private float nextDamageTime = 0f; // Timer for damage application

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
            Debug.LogError("Flame not found! Make sure the flame has the 'Flame' tag.");
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