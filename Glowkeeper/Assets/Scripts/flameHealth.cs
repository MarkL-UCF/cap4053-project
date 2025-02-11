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

    //max flame health
    public float maxFlameFuel;
    
    public Image fuelBar;

 
   

    
    
    // Start is called before the first frame update
    void Start()
    {
        maxFlameFuel = flameFuel;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        fuelBar.fillAmount = Mathf.Clamp(flameFuel / maxFlameFuel, 0, 1);
        
        
        if(flameFuel < 0)
        {
            Destroy(gameObject);
        }
    }

}
