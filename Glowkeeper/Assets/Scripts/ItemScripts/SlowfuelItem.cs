using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlowfuelItem : PlayerItems
{
    //Savor the light
    public override void Info()
    {
        Name = "SlowBurnFuel";
        StatDescription = "+Max Fuel<br>";
    }
    public override void Activate(GameObject parent) 
    {
        Name = "SlowBurnFuel";
        StatDescription = "+Max Fuel<br>";

        var FlameStats = GameObject.FindGameObjectWithTag("Global Stat Tracker (Flame)").GetComponent<FlameStatTracker>();

        FlameStats.maxFlameFuel += 250;
        FlameStats.flameFuel += 250;
    }
}
