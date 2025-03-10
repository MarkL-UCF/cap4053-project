using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WhiskeyItem : PlayerItems
{
    //Double Vision
    public override void Info()
    {
        Name = "Grandpa'sWhiskey";
        StatDescription = "+Projectile Count<br>-Accuracy<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "Grandpa'sWhiskey";
        StatDescription = "+Projectile Count<br>-Accuracy<br>";

        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

        Weapon.numProjectilesFlat += 1;
        Weapon.spread += 5;
        Weapon.UpdateStats();
    }
}
