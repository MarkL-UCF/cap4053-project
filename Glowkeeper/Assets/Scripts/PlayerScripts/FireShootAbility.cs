using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu]
public class FireShootAbility : PlayerAbility
{

    public override void Info()
    {
        Name = "Fire Shooter";
        StatDescription = "Press Spacebar to Activate<br>Shoots Projectiles from the Flame, but drains fuel";
    }
    public override void Activate(GameObject parent)
    {
        //apply change
        var flameScript = GameObject.FindGameObjectWithTag("Flame").GetComponent<FlameAttack>();
        flameScript.shooterOn = true;

    }
    public override void BeginCooldown(GameObject parent)
    {
        var flameScript = GameObject.FindGameObjectWithTag("Flame").GetComponent<FlameAttack>();
        flameScript.shooterOn = false;
        //set back to normal
    }
}

