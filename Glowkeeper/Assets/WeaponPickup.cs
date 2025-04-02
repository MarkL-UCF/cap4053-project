using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeaponPickup : MonoBehaviour
{
    public PlayerItems itemScript;

    private Boolean pickupAllowed;
    public Boolean shopItem;
    private Boolean canBuy;
    public int cost = 0;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI pickUpText;
    public TextMeshProUGUI statsText;


    private void Start()
    {
        pickUpText = GameObject.Find("PickUp").GetComponent<TextMeshProUGUI>();
        statsText = GameObject.Find("StatsDisplay").GetComponent<TextMeshProUGUI>();
        priceText = GameObject.Find("PriceDisplay").GetComponent<TextMeshProUGUI>();
        pickupAllowed = false;
        canBuy = false;
        if (shopItem)
        {
            cost = 10;
        }

    }

    private void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E) && !shopItem)
        {
            ItemHolder item = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>();
            item.CurrentItem = itemScript;
            item.newPickup = true;
            pickUpText.text = "";
            statsText.text = "";
            Destroy(gameObject);
        }
        else if (pickupAllowed && Input.GetKeyDown(KeyCode.E) && shopItem && canBuy)
        {
            playerCurrency currency = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCurrency>();
            currency.SpendCoins(cost);
            ItemHolder item = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>();
            item.CurrentItem = itemScript;
            item.newPickup = true;
            Destroy(gameObject);
            pickUpText.text = "";
            statsText.text = "";
            priceText.text = "";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && !shopItem)
        {
            itemScript.Info();
            pickUpText.text = "Press 'E' to pick up";
            statsText.text = itemScript.Name + ":<br>" + itemScript.StatDescription;
            pickupAllowed = true;


        }
        else if (collision.gameObject.CompareTag("Player") && shopItem)
        {
            playerCurrency currency = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCurrency>();

            itemScript.Info();
            pickUpText.text = "Press 'E' to purchase";
            statsText.text = itemScript.Name + ":<br>" + itemScript.StatDescription;
            pickupAllowed = true;

            if (currency.CheckCoins(cost))
            {
                priceText.faceColor = Color.green;
                priceText.text = "Buy for " + cost;
                canBuy = true;

            }
            else
            {
                priceText.faceColor = Color.red;
                priceText.text = "Buy for " + cost + ":<br>" + "INSUFFICIENT FUNDS";
                canBuy = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpText.text = "";
            statsText.text = "";
            priceText.text = "";
            pickupAllowed = false;
            canBuy = false;
        }
    }
}
