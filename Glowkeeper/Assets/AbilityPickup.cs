using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public PlayerAbility abilityScript;
    private Boolean pickupAllowed;
    public TextMeshProUGUI pickUpText;
    public TextMeshProUGUI statsText;

    private void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            AbilityHolder abilityHold = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityHolder>();
            abilityHold.newAbility = abilityScript;
            abilityHold.newAbilityPickup = true;
            Destroy(gameObject);
            pickUpText.gameObject.SetActive(false);
            statsText.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            abilityScript.Info();
            statsText.text = abilityScript.Name + ":<br>" + abilityScript.StatDescription;
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
