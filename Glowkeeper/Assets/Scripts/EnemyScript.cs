using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   public float moveSpeed = 0.8f; // Movement speed of the enemy
    public float changeDirectionTime = 1f; // Time interval before changing direction

    private Vector3 randomDirection;

    void Start()
    {
        // Set an initial random direction
        SetRandomDirection();
        
        // Start changing direction periodically
        InvokeRepeating("SetRandomDirection", 0f, changeDirectionTime);
    }

    void Update()
    {
        // Move the enemy in the random direction
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
    }

    // Method to set a random direction
    void SetRandomDirection()
    {
        // Randomize the X and Y values for movement direction
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }
}
