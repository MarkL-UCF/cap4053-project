using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 savedVelocity;

    public float projectileSize = 1;
    public float damage = 2;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

        //Handles scaling projectile
        if (projectileSize != 1)
        {
            transform.localScale = (new Vector3(1, 1, 1)) * projectileSize;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb = gameObject.GetComponent<Rigidbody2D>();
        savedVelocity = rb.velocity;


    }

    private void Update()
    {
        
        //Destroy self after a few seconds if it doesn't collide
        timer += Time.deltaTime;
        if (timer > 7)
        {
            Destroy(gameObject);
        }

        if (PauseController.IsGamePaused)
        {
            if (rb.velocity != Vector2.zero)
            {
                savedVelocity = rb.velocity;
            }
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = savedVelocity;
        }
    }

    //Destroy self on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Object collided with");
        Destroy(gameObject);

        // added tag to the enemy object

        //Enemy Shadow Close
       if (collision.gameObject.CompareTag("Enemy"))
        {
            // for all the enemies with the tag enemy
            ShadowSplitter shadowEnemy = collision.gameObject.GetComponent<ShadowSplitter>();
            EnemyScript normalEnemy = collision.gameObject.GetComponent<EnemyScript>();
            SmallerSplitter smallEnemy = collision.gameObject.GetComponent<SmallerSplitter>();

            // Apply damage if the enemy script exists
            if (shadowEnemy != null)
            {
                shadowEnemy.EnemyDamage(damage);
            }
            else if (normalEnemy != null)
            {
                normalEnemy.EnemyDamage(damage);
            }
            else if (smallEnemy != null)
            {
                smallEnemy.EnemyDamage(damage);
            }
            else
            {
                Debug.LogWarning("Enemy tagged object found, but no valid enemy script attached!");
            }

            // Destroy the projectile after applying damage
            Destroy(gameObject);
        }
        //Enemy Shadow Range
        if (collision.gameObject.CompareTag("EnemyRange"))
        {
            // destroy the enemy when hit by a projectile
            Destroy(gameObject);

            // destroy the projectile as well
            Enemy2 enemy = collision.gameObject.GetComponent<Enemy2>();
            enemy.EnemyDamage(damage);
        }
        //Enemy Player Close
        if (collision.gameObject.CompareTag("EnemyPlayer"))
        {
            // destroy the enemy when hit by a projectile
            Destroy(gameObject);

            // destroy the projectile as well
            enemy_player_attack enemy = collision.gameObject.GetComponent<enemy_player_attack>();
            enemy.EnemyDamage(damage);
        }
        //Enemy Player Range
        if (collision.gameObject.CompareTag("EnemyPlayerR"))
        {
            // destroy the enemy when hit by a projectile
            Destroy(gameObject);

            // destroy the projectile as well
            enemy_range_player enemy = collision.gameObject.GetComponent<enemy_range_player>();
            enemy.EnemyDamage(damage);
        }
        //tag enemy projectile object
        else if(collision.gameObject.CompareTag("EnemyProjectile"))//if two opposing projectiles collide, they cancel each other
        {
            //destroy player projectile on impact
            Destroy(gameObject);
            //destroy enemy projectile
            Destroy(collision.gameObject);
        }

    }
}
    