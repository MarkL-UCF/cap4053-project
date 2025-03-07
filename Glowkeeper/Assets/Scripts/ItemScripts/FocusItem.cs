using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FocusItem : PlayerItems
{
    //Precise!
    public override void Activate(GameObject parent)
    {
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        Weapon.damage = 3;
        Weapon.spread = 1;
        Weapon.projectileSize = 0.75f;

    }
}
