using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class flameHealth : MonoBehaviour
{
    //flame health
    public float flameFuel;
    public float prevFuel;

    //max flame health
    public float maxFlameFuel;
    
    public Image fuelBar; //To Do: move fuelBar related stuff to FlameStatTracker so it persists inbetween rooms
    public LightLevelManager LLM;
    public RoomManager roomManager;
    public FlameStatTracker FlameStats;
    
    
    // Start is called before the first frame update
    void Start()
    {
        FlameStats = GameObject.FindGameObjectWithTag("Global Stat Tracker (Flame)").GetComponent<FlameStatTracker>();

        //import stored stats into instantiated object
        flameFuel = FlameStats.flameFuel;
        maxFlameFuel = FlameStats.maxFlameFuel;
        prevFuel = flameFuel;

        LLM = GameObject.FindGameObjectWithTag("Flame").GetComponent<LightLevelManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        fuelBar.fillAmount = Mathf.Clamp(flameFuel / maxFlameFuel, 0, 1);

        if(flameFuel < prevFuel || flameFuel > prevFuel)
        {
            prevFuel = flameFuel;
            ChangeLights();
        }

        KillFlame();
    }


    void ChangeLights()
    {
        LLM.AlterLight(maxFlameFuel, flameFuel);
    }

    public void FlameDamage(int amount)
    {
        flameFuel -= amount;

        //checks if flame is dead
        KillFlame();
    }
    void KillFlame()
    {
        if(flameFuel <= 0)
        {
            Destroy(gameObject);
        }
    }

}
