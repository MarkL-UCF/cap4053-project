using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FocusItem : PlayerItems
{
    //Precise!

    public override void Info()
    {

    }
    public override void Activate(GameObject parent)
    {
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        Weapon.damageFlat += 1;
        Weapon.spreadFlat -= 4;
        Weapon.projectileSizeFlat -= 0.50f; //increased to make the effect more drastic
        Weapon.UpdateStats();

    }
}
