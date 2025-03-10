using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VioletItem : PlayerItems
{
    //High Frequency!

    //Note: this item's counterpart is the Red Lens
    public override void Info()
    {
        
    }
    public override void Activate(GameObject parent) 
    {
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

        //Weapon.firerateFlat -= .33f; //removed for too many changed stats
        Weapon.projectileSpeedFlat += .5f; //changed from 5 to .5, since 5 is double the base speed (which probably was unintentional)
        Weapon.projectileSizeFlat -= .15f; //reduced from original value
        //Weapon.damageFlat -= .2f;  //removed for too many changed stats
        //Weapon.spreadFlat += 5; //removed for too many changed stats
        Weapon.UpdateStats();
    }
}
