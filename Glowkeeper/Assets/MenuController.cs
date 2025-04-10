using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mapCanvas;
    public GameObject menuCanvas;


    private void Start()
    {
        menuCanvas.SetActive(false);
        mapCanvas.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            mapCanvas.SetActive(!mapCanvas.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
