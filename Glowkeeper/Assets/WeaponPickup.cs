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
    public TextMeshProUGUI pickUpText;
    public TextMeshProUGUI statsText;


    private void Start()
    {
        pickUpText = GameObject.Find("PickUp").GetComponent<TextMeshProUGUI>();
        statsText = GameObject.Find("StatsDisplay").GetComponent<TextMeshProUGUI>();
        pickupAllowed = false;
        
        
    }

    private void Update()
    {
        pickUpText = GameObject.Find("PickUp").GetComponent<TextMeshProUGUI>();
        statsText = GameObject.Find("StatsDisplay").GetComponent<TextMeshProUGUI>();

        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            ItemHolder item = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>();
            item.CurrentItem = itemScript;
            item.newPickup = true;
            pickUpText.text = "";
            statsText.text = "";
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            itemScript.Info();
            pickUpText.text = "Press 'E' to pick up";
            statsText.text = itemScript.Name + ":<br>" + itemScript.StatDescription;
            pickupAllowed = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpText.text = "";
            statsText.text = "";
            pickupAllowed = false;
        }
    }
}
