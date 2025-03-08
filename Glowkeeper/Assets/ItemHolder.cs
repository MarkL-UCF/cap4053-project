using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    
    public PlayerItems CurrentItem;
    public PlayerItems[] Items;
    int numItems;
    public Boolean newPickup;

    private void Start()
    {
        newPickup = false;
        Items = new PlayerItems[10];
        numItems = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (newPickup)
        {
            CurrentItem.Info();


            if (numItems == 0)
            {
                Items[0] = CurrentItem;
                CurrentItem.Activate(gameObject);
                newPickup = false;
                numItems = 1;
            }
            else
            {
                for (int i = 0; i < numItems; i++)
                {
                    if (CurrentItem.name == Items[i].name)
                    {
                        newPickup = false;
                        return;
                    }
                    else if (i == numItems - 1)
                    {
                        Items[i+1] = CurrentItem;
                        CurrentItem.Activate(gameObject);
                        newPickup = false;
                        numItems++;
                    }

                }
            }
        }
    }
    
}
