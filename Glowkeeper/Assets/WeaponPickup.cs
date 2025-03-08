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
        pickupAllowed = false;
        pickUpText.gameObject.SetActive(false);
        
    }

    private void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            ItemHolder item = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>();
            item.CurrentItem = itemScript;
            item.newPickup = true;
            Destroy(gameObject);
            pickUpText.gameObject.SetActive(false);
            statsText.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            itemScript.Info();
            statsText.text = itemScript.Name + ":<br>" + itemScript.StatDescription;
            pickUpText.gameObject.SetActive(true);
            statsText.gameObject.SetActive(true);
            pickupAllowed = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            statsText.gameObject.SetActive(false);
            pickupAllowed = false;
        }
    }
}
