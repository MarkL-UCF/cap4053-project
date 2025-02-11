using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    private Vector3 rotation;
    public Vector3 mousePos;
    public float projectileSpeed = 300f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
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

    }
}
    