using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelRefill : MonoBehaviour
{
    public float fuelAmt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            flameHealth Fuel = GameObject.FindGameObjectWithTag("Flame").GetComponent<flameHealth>();
            Fuel.flameFuel += fuelAmt;

            if(Fuel.flameFuel > Fuel.maxFlameFuel)
            {
                Fuel.flameFuel = Fuel.maxFlameFuel;
            }

            Destroy(gameObject);
        }
    }
}
