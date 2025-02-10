using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHealth : MonoBehaviour
{
    private Light2D Primarylight;
    private float fuelLoss;
    private float fuelDiff = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        Primarylight.falloffIntensity = 0f;

    }

    // Update is called once per frame
    public void alterLight(float maxFuel, float fuel)
    {
        fuelLoss = maxFuel - fuel;
        fuelDiff = fuelLoss - fuelDiff;

        Primarylight.falloffIntensity += fuelDiff;

        

    }
}
