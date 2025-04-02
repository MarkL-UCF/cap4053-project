using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelRefill : MonoBehaviour
{
    public float fuelAmt;
    public Boolean shopItem;
    public Boolean canBuy;
    public Boolean canPickup;
    public int cost = 0;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI pickUpText;

    private void Start()
    {
        if(shopItem)
        {
            cost = 10;
        }
        
        canBuy = false;
        canPickup = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBuy && canPickup)
        {
            playerCurrency currency = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCurrency>();
            currency.SpendCoins(cost);
            flameHealth Fuel = GameObject.FindGameObjectWithTag("Flame").GetComponent<flameHealth>();
            Fuel.flameFuel += fuelAmt;

            if (Fuel.flameFuel > Fuel.maxFlameFuel)
            {
                Fuel.flameFuel = Fuel.maxFlameFuel;
            }

            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !shopItem)
        {
            playerCurrency currency = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCurrency>();
            currency.SpendCoins(cost);
            flameHealth Fuel = GameObject.FindGameObjectWithTag("Flame").GetComponent<flameHealth>();
            Fuel.flameFuel += fuelAmt;

            if (Fuel.flameFuel > Fuel.maxFlameFuel)
            {
                Fuel.flameFuel = Fuel.maxFlameFuel;
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && shopItem)
        {
            canPickup = true;
            playerCurrency currency = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCurrency>();
            pickUpText = GameObject.Find("PickUp").GetComponent<TextMeshProUGUI>();
            priceText = GameObject.Find("PriceDisplay").GetComponent<TextMeshProUGUI>();
            pickUpText.text = "Press 'E' to purchase";

            if (currency.CheckCoins(cost))
            {
                canBuy = canPickup;
                priceText.faceColor = Color.green;
                priceText.text = "Buy for " + cost;

            }
            else
            {
                priceText.faceColor = Color.red;
                priceText.text = "Buy for " + cost + ":<br>" + "INSUFFICIENT FUNDS";

            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPickup = false;
            canBuy = false;
            priceText.text = "";
            pickUpText.text = "";
        }
    }
}