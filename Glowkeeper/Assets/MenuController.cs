using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mapCanvas;
    public GameObject menuCanvas;
    public GameObject deadMenu;


    private void Start()
    {
        menuCanvas.SetActive(false);
        mapCanvas.SetActive(false);
        deadMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mapCanvas.activeSelf && PauseController.IsGamePaused)
            {
                return;
            }

            mapCanvas.SetActive(!mapCanvas.activeSelf);
            PauseController.SetPause(mapCanvas.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuCanvas.activeSelf && PauseController.IsGamePaused)
            {
                return;
            }
            menuCanvas.SetActive(true);
            PauseController.SetPause(true);
        }
    }

    public void PlayerDied()
    {
        deadMenu.SetActive(true);
    }
}
