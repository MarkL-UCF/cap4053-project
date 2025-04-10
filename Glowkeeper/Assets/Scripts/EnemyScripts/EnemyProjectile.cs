using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float lifetime = 3f; // Destroy after 5 seconds
    public int damage = 30;
    public Vector2 savedVelocity;
    public Rigidbody2D projRb;

    void Start()
    {
        // Destroy projectile after a certain time to prevent memory issues
        projRb = gameObject.GetComponent<Rigidbody2D>();
        savedVelocity = projRb.velocity;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if(PauseController.IsGamePaused)
        {
            if (projRb.velocity != Vector2.zero)
            {
                savedVelocity = projRb.velocity; 
            }
            projRb.velocity = Vector2.zero;
        }
        else
        {
            projRb.velocity = savedVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hits the player
        if (collision.CompareTag("Flame") )
        {
            Debug.Log("Flame hit!");
            // TODO: Implement Flame damage logic
            flameHealth Flame = GameObject.FindGameObjectWithTag("Flame").GetComponent<flameHealth>();
            Flame.FlameDamage(damage);
            Destroy(gameObject); // Destroy the projectile on impact
        }
        else if(collision.CompareTag("Player")){
            Debug.Log("Player hit!");
            playerHealth Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
            Player.PlayerDamage(0.5f);
            Destroy(gameObject); 
        }
        else if(collision.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
       
    }
}