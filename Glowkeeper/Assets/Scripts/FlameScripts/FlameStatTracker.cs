using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStatTracker : MonoBehaviour
{
    //This stores the stats of the flame to help instantiate the flame in each room
    public float flameFuel;
    public float maxFlameFuel;
    public bool isExtinguished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Store fuel at the end of room
    void StoreFuel(float fuel)
    {
        flameFuel = fuel; 
    }
}
