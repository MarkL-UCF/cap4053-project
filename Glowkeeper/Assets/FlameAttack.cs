using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : MonoBehaviour
{
    public Boolean shooterOn = false;
    public GameObject bullet;
    public float fireRate = 5f;
    public float projectileSpeed;
    float timer = 0f;
    private float lastShotTime = 0f;
    

    // Update is called once per frame
    void Update()
    {
        flameHealth flameHealth = GameObject.FindGameObjectWithTag("Flame").GetComponent<flameHealth>();
        if (shooterOn)
        {
            Vector3 shootDirection1 = new Vector3(1, 0, 0).normalized;
            Vector3 shootDirection2 = new Vector3(-1, 0, 0).normalized;
            Vector3 shootDirection3 = new Vector3(0, 1, 0).normalized;
            Vector3 shootDirection4 = new Vector3(0, -1, 0).normalized;

            // Spawn position slightly in front of the enemy
            Vector3 spawnPosition1 = transform.position + new Vector3(shootDirection1.x * 0.5f, shootDirection1.y * 0.5f, 0);
            Vector3 spawnPosition2 = transform.position + new Vector3(shootDirection2.x * 0.5f, shootDirection2.y * 0.5f, 0);
            Vector3 spawnPosition3 = transform.position + new Vector3(shootDirection3.x * 0.5f, shootDirection3.y * 0.5f, 0);
            Vector3 spawnPosition4 = transform.position + new Vector3(shootDirection4.x * 0.5f, shootDirection4.y * 0.5f, 0);

            // Instantiate the projectile
            GameObject projectile1 = Instantiate(bullet, spawnPosition1, Quaternion.identity);
            GameObject projectile2 = Instantiate(bullet, spawnPosition2, Quaternion.identity);
            GameObject projectile3 = Instantiate(bullet, spawnPosition3, Quaternion.identity);
            GameObject projectile4 = Instantiate(bullet, spawnPosition4, Quaternion.identity);

            // Get Rigidbody2D of the projectile
            Rigidbody2D projRb1 = projectile1.GetComponent<Rigidbody2D>();
            Rigidbody2D projRb2 = projectile2.GetComponent<Rigidbody2D>();
            Rigidbody2D projRb3 = projectile3.GetComponent<Rigidbody2D>();
            Rigidbody2D projRb4 = projectile4.GetComponent<Rigidbody2D>();

            if (projRb1 != null)
            {
                // Apply velocity to the projectile
                projRb1.velocity = shootDirection1 * projectileSpeed;
            }
            if (projRb2 != null)
            {
                // Apply velocity to the projectile
                projRb2.velocity = shootDirection2 * projectileSpeed;
            }
            if (projRb3 != null)
            {
                // Apply velocity to the projectile
                projRb3.velocity = shootDirection3 * projectileSpeed;
            }
            if (projRb4 != null)
            {
                // Apply velocity to the projectile
                projRb4.velocity = shootDirection4 * projectileSpeed;
            }

            flameHealth.FlameDamage(25);
        }
    }
}
