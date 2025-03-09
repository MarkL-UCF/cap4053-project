using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PepperItem : PlayerItems
{
    //Feel the heat!
    public override void Info()
    {
        Name = "SpicyPepper";
        StatDescription = "+Move Speed<br>Restore Fuel<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "SpicyPepper";
        StatDescription = "+Move Speed<br>+Restore Fuel<br>";

        var Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        var FlameStats = GameObject.FindGameObjectWithTag("Global Stat Tracker (Flame)").GetComponent<FlameStatTracker>();

        Player.movespeedFlat += .5f;
        FlameStats.flameFuel += 500;
        
        Player.UpdateStats();
    }
}
