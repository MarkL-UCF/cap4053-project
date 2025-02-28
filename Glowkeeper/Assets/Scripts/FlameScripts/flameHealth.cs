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
        LLM = GameObject.FindGameObjectWithTag("Flame").GetComponent<LightLevelManager>();

        //import stored stats into instantiated object
        flameFuel = FlameStats.flameFuel;
        maxFlameFuel = FlameStats.maxFlameFuel;
        prevFuel = flameFuel;

        //Debug.Log("Flame Instantiated");
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


    public void ChangeLights()
    {
        LLM.AlterLight(flameFuel, maxFlameFuel);
    }

    public void FlameDamage(int amount)
    {
        flameFuel -= amount;

        //checks if flame is dead
        KillFlame();
    }

    //Called when flame runs out of fuel
    void KillFlame()
    {
        if(flameFuel <= 0)
        {
            FlameStats.isExtinguished = true; //prevent later rooms from reinstantiating the flame
            LLM.AlterLight(0, 1); //turn out the lights
            Destroy(gameObject); //destroy the instantiated game object
        }
    }

    //Called at the end of wave 
    public void DespawnFlame()
    {
        FlameStats.flameFuel = flameFuel; //record fuel

        LLM.AlterLight(1, 1); //restore light to how it is out of combat

        Destroy(gameObject); //destroy the instantiated game object
    }

}
