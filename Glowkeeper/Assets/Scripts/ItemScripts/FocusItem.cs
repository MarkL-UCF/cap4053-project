using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FocusItem : PlayerItems
{
    //Precise!

    public override void Info()
    {
        Name = "FocusLens";
        StatDescription = "+Damage<br>+Accuracy<br>-Size<br>";
    }
    public override void Activate(GameObject parent)
    {
        Name = "FocusLens";
        StatDescription = "+Damage<br>+Accuracy<br>-Size<br>";

        var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        Weapon.damageFlat += 1;
        Weapon.spreadFlat -= 4;
        Weapon.projectileSizeFlat -= 0.25f;
        Weapon.UpdateStats();

    }
}
