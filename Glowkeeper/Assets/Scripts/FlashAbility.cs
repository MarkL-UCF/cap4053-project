using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu]
public class FlashAbility : PlayerAbility
{
   
    public float lastUse = 0f;
    public override void Activate(GameObject parent)
    {
        Debug.Log("Ability Activated!");
        Light2D Flash = GameObject.FindGameObjectWithTag("Flash").GetComponent<Light2D>();
        EnemyScript Enemy1 = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
        Enemy2 Enemy2 = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy2>();
        //apply change
        Flash.intensity = 10;

        //Enemies are slowed/slower fire rate
        Enemy1.moveSpeed -= 0.3f;
        Enemy2.moveSpeed -= 0.3f;

        Enemy1.fireRate += 0.5f;
        Enemy2.fireRate += 0.5f;

        lastUse = Time.time;

        

    }
    public override void BeginCooldown(GameObject parent)
    {
        Light2D Flash = GameObject.FindGameObjectWithTag("Flash").GetComponent<Light2D>();
        EnemyScript Enemy1 = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
        Enemy2 Enemy2 = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy2>();
        Flash.intensity = 0;
        //set back to normal
        Enemy1.moveSpeed += 0.3f;
        Enemy2.moveSpeed += 0.3f;

        Enemy1.fireRate -= 0.5f;
        Enemy2.fireRate -= 0.5f;


    }
}

