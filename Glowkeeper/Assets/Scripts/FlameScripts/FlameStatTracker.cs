using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameStatTracker : MonoBehaviour
{
    //This stores the stats of the flame to help instantiate the flame in each room
    public float flameFuel;
    public float maxFlameFuel;
    public Image fuelBar;
    public bool isExtinguished = false;

    // Start is called before the first frame update
    void Start()
    {
        fuelBar = GameObject.Find("Fuel Bar UI").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fuelBar.fillAmount = Mathf.Clamp(flameFuel / maxFlameFuel, 0, 1);
    }

    // Store fuel at the end of room
    void StoreFuel(float fuel)
    {
        flameFuel = fuel; 
    }
}
