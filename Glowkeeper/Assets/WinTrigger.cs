using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{


    [SerializeField] GameObject winCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Player reached the end! You win!");
        }
    }


}
