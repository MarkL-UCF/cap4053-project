using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public string Type;
    private int amt;

    private void Start()
    {
        switch (Type)
        {
            case "Gold":
                amt = 10;

                break;
            case "Silver":
                amt = 5;

                break;
            case "Bronze":
                amt = 1;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCurrency Currency = collision.gameObject.GetComponent<playerCurrency>();
            Currency.ReceiveCoins(amt);

            Destroy(gameObject);
        }
    }
}
