using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure : MonoBehaviour
{
    // Start is called before the first frame update

    // Start is called before the first frame update
    public ItemSpawner itemSpawner;
    public int numberOfItems = 1;

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
            Vector3 offset = new Vector3((i * 2.0f), 0, 0);
            Vector3 spawnPos = transform.position + offset;
            itemSpawner.SpawnPassiveAndAbilitiesAt(spawnPos);
        }
    }




    // Update is called once per frame
    void Update()
    {

    }
}
