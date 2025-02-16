using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHealth : MonoBehaviour
{
    private Light2D Primarylight;


    // Start is called before the first frame update
    void Start()
    {
        Primarylight = GetComponent<Light2D>();

        Primarylight.falloffIntensity = 0f;

    }

    // Update is called once per frame
    public void AlterLight(float maxFuel, float fuel)
    {
        Primarylight.pointLightOuterRadius = (fuel * 0.1f);



    }
}
