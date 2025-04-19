using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemSpawner itemSpawner;
    public int numberOfItems = 4;

    void Start()
    {
        if (itemSpawner == null)
        {
            itemSpawner = FindObjectOfType<ItemSpawner>();

        }

        SpawnTreasure();
    }

void SpawnTreasure()
{

    for (int i = 0; i < numberOfItems; i++)
    {
        itemSpawner = FindObjectOfType<ItemSpawner>();

        Vector3 offset = new Vector3((i * 2.0f) - 3, 0, 0);
        Vector3 spawnPos = transform.position + offset;

        if (i < 2)
            itemSpawner.ShopSpawnItemAt(spawnPos);
        else
            itemSpawner.ShopSpawnPickupAt(spawnPos); 
    }
}





    // Update is called once per frame
    void Update()
    {

        
    }
}
