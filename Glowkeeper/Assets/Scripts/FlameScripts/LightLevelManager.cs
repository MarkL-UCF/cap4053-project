using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightLevelManager : MonoBehaviour
{
    private GameObject lightFull;
    private GameObject light75;
    private GameObject light50;
    private GameObject light25;


    // Start is called before the first frame update
    void Start()
    {
        lightFull = GameObject.FindGameObjectWithTag("Player Light");
        light75 = GameObject.FindGameObjectWithTag("Light Ring (75%)");
        light50 = GameObject.FindGameObjectWithTag("Light Ring (50%)");
        light25 = GameObject.FindGameObjectWithTag("Light Ring (25%)");
    }

    // Update is called once per frame
    public void AlterLight(float fuel, float maxFuelHealth)
    {
        float fuelLevel = Mathf.Clamp(fuel / maxFuelHealth, 0, 1);

        if (fuel == 0)
        {
            lightFull.SetActive(false);
            light75.SetActive(false);
            light50.SetActive(false);
            light25.SetActive(false);
        }
        else if (fuel < .25)
        {
            lightFull.SetActive(false);
            light75.SetActive(false);
            light50.SetActive(false);
            light25.SetActive(true);
        }
        else if (fuel < .50)
        {
            lightFull.SetActive(false);
            light75.SetActive(false);
            light50.SetActive(true);
            light25.SetActive(true);
        }
        else if (fuel < .75)
        {
            lightFull.SetActive(false);
            light75.SetActive(true);
            light50.SetActive(true);
            light25.SetActive(true);
        }
        else
        {
            lightFull.SetActive(true);
            light75.SetActive(false);
            light50.SetActive(false);
            light25.SetActive(false);
        }
    }
}
