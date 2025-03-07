using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public PlayerAbility abilityScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AbilityHolder abilityHold = collision.gameObject.GetComponent<AbilityHolder>();
            abilityHold.newAbility = abilityScript;
            abilityHold.newAbilityPickup = true;
            Destroy(gameObject);
        }
    }
}
