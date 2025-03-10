using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashScript : MonoBehaviour
{
    
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Light2D>().enabled = false;
        gameObject.GetComponent<Light2D>().intensity = 10;
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (gameObject.GetComponent<Light2D>().intensity == 10 && enabled)
        {
            if (timer > .5)
            {
                gameObject.GetComponent<Light2D>().enabled = false;
                gameObject.GetComponent<Light2D>().intensity = .5f;

            }
        }
        if (gameObject.GetComponent<Light2D>().intensity == .5)
        {
            if (timer >= .8 && timer < 1.1)
            {
                gameObject.GetComponent<Light2D>().enabled = true;
            }
            else if (timer >= 1.1 && timer < 1.6)
            {
                gameObject.GetComponent<Light2D>().enabled = false;
            }
            else if (timer >= 2.1 && timer < 2.4)
            {
                gameObject.GetComponent<Light2D>().enabled = true;
            }
            else if (timer >= 2.4 && timer < 2.9)
            {
                gameObject.GetComponent<Light2D>().enabled = false;
            }
            else if (timer >= 2.9 && timer < 3.2)
            {
                gameObject.GetComponent<Light2D>().enabled = true;
            }
            else if (timer >= 3.2 && timer < 3.7)
            {
                gameObject.GetComponent<Light2D>().enabled = false;
            }
            else if (timer >= 3.7 && timer < 4)
            {
                gameObject.GetComponent<Light2D>().enabled = true;
            }
            else if (timer >= 4)
            {
                gameObject.GetComponent<Light2D>().enabled = false;
                gameObject.GetComponent<Light2D>().intensity = 10;
            }

        }
    }

}
