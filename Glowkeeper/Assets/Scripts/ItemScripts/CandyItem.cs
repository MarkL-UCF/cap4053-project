using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CandyItem : MonoBehaviour
{
    public Boolean shopItem;
    public Boolean canBuy;
    public Boolean canPickup;
    public int cost = 0;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI pickUpText;

    private void Start()
    {
        canBuy = false;
        canPickup = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBuy && canPickup)
        {
            playerCurrency currency = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCurrency>();
            currency.SpendCoins(cost);
            playerHealth Health = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
           

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !shopItem)
        {
            var Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();

            int val = UnityEngine.Random.Range(1, 5);

            switch (val)
            {
                case 1:
                    Weapon.damageFlat += 0.25f;
                    break;

                case 2:
                    Weapon.firerateFlat -= 0.20f;
                    break;

                case 3:
                    Weapon.projectileSpeedFlat += 0.20f;
                    break;

                case 4:
                    Weapon.spreadFlat -= 1;
                    break;

                case 5:
                    Weapon.projectileSizeFlat += .1f;
                    break;
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
}
