using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public PlayerItems CurrentItem;
    public PlayerItems[] Items;
    public Boolean newPickup;

    private void Start()
    {
        newPickup = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (newPickup)
        {
            int i = 0;
            CurrentItem.Info();

            
            for (i = 0; i < Items.Length; i++)
            {
                if (CurrentItem.Name == Items[i].Name)
                {
                    newPickup = false;
                }
                else if (i == Items.Length - 1)
                {
                    Items[i + 1] = CurrentItem;
                    CurrentItem.Activate(gameObject);
                    newPickup = false;
                }

            }
        }
    }
}
