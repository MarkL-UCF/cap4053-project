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
        lightFull = GameObject.FindGameObjectWithTag("Player Light"); //note that if the playerLight is already disabled, this will break the code
        light75 = gameObject.transform.Find("Light 75%").gameObject;
        light50 = gameObject.transform.Find("Light 50%").gameObject;
        light25 = gameObject.transform.Find("Light 25%").gameObject;
    }

    // Update is called once per frame
    public void AlterLight(float fuel, float maxFuelHealth)
    {
        float fuelLevel = Mathf.Clamp(fuel / maxFuelHealth, 0, 1);

        Debug.Log("Fuel percentage is " + fuelLevel);

        if (fuelLevel <= 0)
        {
            //Debug.Log("Light Level = 0%");
            lightFull.SetActive(false);
            light75.SetActive(false);
            light50.SetActive(false);
            light25.SetActive(false);
        }
        else if (fuelLevel < .25)
        {
            //Debug.Log("Light Level = 25%");
            lightFull.SetActive(false);
            light75.SetActive(false);
            light50.SetActive(false);
            light25.SetActive(true);
        }
        else if (fuelLevel < .50)
        {
            //Debug.Log("Light Level = 50%");
            lightFull.SetActive(false);
            light75.SetActive(false);
            light50.SetActive(true);
            light25.SetActive(true);
        }
        else if (fuelLevel < .75)
        {
            //Debug.Log("Light Level = 75%");
            lightFull.SetActive(false);
            light75.SetActive(true);
            light50.SetActive(true);
            light25.SetActive(true);
        }
        else
        {
            //Debug.Log("Light Level = 100%");
            lightFull.SetActive(true);
            light75.SetActive(false);
            light50.SetActive(false);
            light25.SetActive(false);
        }
    }
}
