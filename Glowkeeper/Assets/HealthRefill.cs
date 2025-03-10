using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRefill : MonoBehaviour
{
    public float healAmt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerHealth Health = collision.gameObject.GetComponent<playerHealth>();
            Health.health += healAmt;

            if (Health.health > Health.maxHealth)
            {
                Health.health = Health.maxHealth;
            }

            Destroy(gameObject);
        }
    }
}
