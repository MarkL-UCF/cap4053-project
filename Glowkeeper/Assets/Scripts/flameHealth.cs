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
    
    public Image fuelBar;

 
    LightHealth primaryLight;

    
    
    // Start is called before the first frame update
    void Start()
    {
        maxFlameFuel = flameFuel;
        prevFuel = flameFuel;
        primaryLight = GameObject.FindGameObjectWithTag("PrimeLight").GetComponent<LightHealth>();  
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
        primaryLight.AlterLight(maxFlameFuel, flameFuel);
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
