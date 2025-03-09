using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RedItem : PlayerItems
{
    //Low Frequency!

    //Note: this item's counterpart is the Violet Lens
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

        Weapon.projectileSizeFlat += .25f; //reduced from original value
        Weapon.projectileSpeedFlat -= .2f;
        //Weapon.firerateFlat += .25f; //removed for too many changed stats
        //Weapon.damageFlat += .5f;  //removed for too many changed stats
        Weapon.UpdateStats();
    }
}
