using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpamItem : PlayerItems
{
    //Quantity over quality
    public override void Info()
    {
        Name = "Spicy Ham";
        StatDescription = "+Fire Rate <br>-Damage<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "Spicy Ham";
        StatDescription = "+Fire Rate <br>-Damage<br>";

        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

        Weapon.firerateScalar -= .5f;
        Weapon.damageScalar -= .2f;
        Weapon.UpdateStats();
    }
}
