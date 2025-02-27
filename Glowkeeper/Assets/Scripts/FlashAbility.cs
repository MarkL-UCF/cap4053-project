using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu]
public class FlashAbility : PlayerAbility
{

    private GameObject[] Enemies;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Ability Activated!");

        Light2D Flash = GameObject.FindGameObjectWithTag("Flash").GetComponent<Light2D>();
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //apply change
        Flash.enabled = true;

        //Enemies are slowed/slower fire rate
        foreach (GameObject enemy in Enemies)
        {
            EnemyScript enemy1 = enemy.GetComponent<EnemyScript>();
            Enemy2 enemy2 = enemy.gameObject.GetComponent<Enemy2>();
            enemy1.moveSpeed -= 0.8f;
            enemy2.moveSpeed -= 0.8f;
            enemy1.fireRate += 0.8f;
            enemy2.fireRate += 0.8f;


            if(enemy1.isShadow)
            {
                enemy1.EnemyDamage(2);
            }
            if (enemy2.isShadow)
            {
                enemy2.EnemyDamage(2);
            }

        }

    }
    public override void BeginCooldown(GameObject parent)
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Set back to normal stats
        foreach (GameObject enemy in Enemies)
        {
            EnemyScript enemy1 = enemy.GetComponent<EnemyScript>();
            Enemy2 enemy2 = enemy.gameObject.GetComponent<Enemy2>();
            enemy1.moveSpeed += 0.8f;
            enemy2.moveSpeed += 0.8f;
            enemy1.fireRate -= 0.8f;
            enemy2.fireRate -= 0.8f;
        }


    }
}

