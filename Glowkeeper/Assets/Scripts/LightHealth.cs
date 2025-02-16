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
        Primarylight.pointLightOuterRadius = 9.6f;
        Primarylight.pointLightInnerRadius = 4;
        Primarylight.intensity = 1;
    }

    // Update is called once per frame
    public void AlterLight(float maxFuel, float fuel)
    {
        Primarylight.pointLightOuterRadius = (fuel * 0.09f);
        Primarylight.pointLightInnerRadius = (fuel * 0.05f);

        if(fuel < 50)
        {
            Primarylight.intensity = (fuel * 0.02f);
        }
        else
        {
            Primarylight.intensity = 1;
        }


    }
}
