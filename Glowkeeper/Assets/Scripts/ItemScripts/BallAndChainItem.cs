using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BallAndChainItem : PlayerItems
{
    //Feel the heat!
    public override void Info()
    {
        Name = "BallAndChain";
        StatDescription = "+Damage<br>-Movespeed<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "BallAndChain";
        StatDescription = "+Damage<br>-Movespeed<br>";

        var Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

        Player.movespeedFlat -= .25f;
        Weapon.damageFlat += 1;
        
        Player.UpdateStats();
    }
}
