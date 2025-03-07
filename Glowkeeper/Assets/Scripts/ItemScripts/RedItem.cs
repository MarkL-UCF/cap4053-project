using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RedItem : PlayerItems
{
    //Low Frequency!
    public override void Activate(GameObject parent) 
    {
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        Weapon.projectileSize = 2f;
        Weapon.damage = 3;
        Weapon.projectileSpeed = 4;
        Weapon.firerate = 1.25f;
    }
}
