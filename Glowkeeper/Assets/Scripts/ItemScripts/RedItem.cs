using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RedItem : PlayerItems
{
    //Low Frequency!

    public override void Info()
    {
        Name = "RedLens";
        StatDescription = "+Damage<br>-Fire Rate<br>-Speed<br>+Size<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "RedLens";
        StatDescription = "+Damage<br>-Fire Rate<br>-Speed<br>+Size<br>";

        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

        Weapon.projectileSizeFlat += .33f;
        Weapon.projectileSpeedFlat -= .2f;
        Weapon.firerateFlat += .25f;
        Weapon.damageFlat += .5f;
        Weapon.UpdateStats();
    }
}
