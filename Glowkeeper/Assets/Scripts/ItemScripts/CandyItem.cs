using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CandyItem : PlayerItems
{
    //Precise!
    public override void Activate(GameObject parent)
    {
        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

        int val = Random.Range(1, 5);

        switch (val)
        {
            case 1:
                Weapon.damageFlat += 0.25f;
                break;

            case 2:
                Weapon.firerateFlat += 0.20f;
                break;

            case 3:
                Weapon.projectileSpeedFlat += 0.20f;
                break;

            case 4:
                Weapon.spreadFlat -= 1;
                break;

            case 5:
                Weapon.projectileSizeFlat += .1f;
                break;
        }

    }
}
