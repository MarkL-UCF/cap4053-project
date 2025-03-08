using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VioletItem : PlayerItems
{
    //High Frequency!

    public override void Info()
    {
        Name = "VioletLens";
        StatDescription = "-Damage<br>-Accuracy<br>+Fire Rate<br>+Speed<br>-Size<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "VioletLens";
        StatDescription = "-Damage<br>-Accuracy<br>+Fire Rate<br>+Speed<br>-Size<br>";

        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        Weapon.firerateFlat -= .33f;
        Weapon.damageFlat -= .2f;
        Weapon.projectileSpeedFlat += 5;
        Weapon.projectileSizeFlat -= .33f;
        Weapon.spreadFlat += 5;
        Weapon.UpdateStats();
    }
}
