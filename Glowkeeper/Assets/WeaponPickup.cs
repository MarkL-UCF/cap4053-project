using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public PlayerItems itemScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            ItemHolder item = collision.gameObject.GetComponent<ItemHolder>();
            item.CurrentItem = itemScript;
            item.newPickup = true;
            Destroy(gameObject);
        }
    }
}
