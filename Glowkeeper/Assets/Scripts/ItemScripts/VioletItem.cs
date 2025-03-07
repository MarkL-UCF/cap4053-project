using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VioletItem : PlayerItems
{
    //High Frequency!
    public override void Activate(GameObject parent) 
    {
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        Weapon.firerate = .5f;
        Weapon.damage = 1;
        Weapon.projectileSpeed = 10;
        Weapon.projectileSize = .5f;
        Weapon.spread = 20;
    }
}
