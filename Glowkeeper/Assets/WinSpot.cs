using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSpot : MonoBehaviour
{
    Metrics metrics;
    // Start is called before the first frame update
    void Start()
    {
        metrics = GameObject.FindGameObjectWithTag("Metrics").GetComponent<Metrics>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(collider.gameObject);
            metrics.playerWon();
        }
    }
}
