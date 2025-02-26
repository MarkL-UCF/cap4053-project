using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    public float projectileSize = 1;
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

        
    }

    private void Update()
    {
        
        //Destroy self after a few seconds if it doesn't collide
        timer += Time.deltaTime;
        if (timer > 7)
        {
            Destroy(gameObject);
        }
    }

    //Destroy self on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Object collided with");
        Destroy(gameObject);

        // added tag to the enemy object
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // destroy the enemy when hit by a projectile
            Destroy(gameObject);

            // destroy the projectile as well
            Destroy(collision.gameObject);
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
    