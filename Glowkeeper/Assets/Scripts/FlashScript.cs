using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashScript : MonoBehaviour
{
    float lastUse = 0f;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Light2D>().enabled = false;
        gameObject.GetComponent<Light2D>().intensity = 10;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            gameObject.GetComponent<Light2D>().enabled = false;
        }
    }

}
