using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu]
public class FlashAbility : PlayerAbility
{

    private GameObject[] EnemiesClose;
    private GameObject[] EnemiesRange;
    private GameObject[] EnemiesPlayer;
    private GameObject[] EnemiesPlayerR;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Ability Activated!");

        Light2D Flash = GameObject.FindGameObjectWithTag("Flash").GetComponent<Light2D>();
        var flashScript = GameObject.FindGameObjectWithTag("Flash").GetComponent<FlashScript>();
        EnemiesClose = GameObject.FindGameObjectsWithTag("Enemy");
        EnemiesRange = GameObject.FindGameObjectsWithTag("EnemyRange");
        EnemiesPlayer = GameObject.FindGameObjectsWithTag("EnemyPlayer");
        EnemiesPlayerR = GameObject.FindGameObjectsWithTag("EnemyPlayerR");

        //apply change
        Flash.enabled = true;
        flashScript.timer = 0;
        

        //Enemies are slowed/slower fire rate

        //Shadow Enemies Close
        foreach (GameObject enemy in EnemiesClose)
        {
            EnemyScript enemy1 = enemy.GetComponent<EnemyScript>();
            enemy1.moveSpeed = 0f;

            enemy1.EnemyDamage(1);
           
        }

        foreach(GameObject enemy in EnemiesRange)
        {
            Enemy2 enemy2 = enemy.GetComponent<Enemy2>();
            enemy2.moveSpeed -= .8f;
            enemy2.fireRate += 2f;

            enemy2.EnemyDamage(1);

        }

        foreach (GameObject enemy in EnemiesPlayer)
        {
            enemy_player_attack enemy3 = enemy.GetComponent<enemy_player_attack>();
            enemy3.moveSpeed -= .8f;
            enemy3.damageRate += 2;
            

        }
        foreach (GameObject enemy in EnemiesPlayerR)
        {
            enemy_range_player enemy4 = enemy.GetComponent<enemy_range_player>();
            enemy4.fireRate += 2f;

        }

    }
    public override void BeginCooldown(GameObject parent)
    {
        EnemiesClose = GameObject.FindGameObjectsWithTag("Enemy");
        EnemiesRange = GameObject.FindGameObjectsWithTag("EnemyRange");
        EnemiesPlayer = GameObject.FindGameObjectsWithTag("EnemyPlayer");
        EnemiesPlayerR = GameObject.FindGameObjectsWithTag("EnemyPlayerR");

        //Set back to normal stats
        foreach (GameObject enemy in EnemiesClose)
        {
            EnemyScript enemy1 = enemy.GetComponent<EnemyScript>();
            enemy1.moveSpeed = 0.8f;

        }

        foreach (GameObject enemy in EnemiesRange)
        {
            Enemy2 enemy2 = enemy.GetComponent<Enemy2>();
            enemy2.moveSpeed += 0.8f;
            enemy2.fireRate -= 2f;

        }

        foreach (GameObject enemy in EnemiesPlayer)
        {
            enemy_player_attack enemy3 = enemy.GetComponent<enemy_player_attack>();
            enemy3.moveSpeed += 0.8f;
            enemy3.damageRate -= 2;

        }

        foreach (GameObject enemy in EnemiesPlayerR)
        {
            enemy_range_player enemy4 = enemy.GetComponent<enemy_range_player>();
            enemy4.fireRate -= 2f;

        }

    }
}

