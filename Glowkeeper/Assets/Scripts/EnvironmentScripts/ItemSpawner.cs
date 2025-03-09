using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemAtlas itemAtlas;

    public ArrayList passiveItemDeck;

    float[] chances = {.69f, .20f, .10f, .1f};
    //69% chance of nothing spawning
    //20% chance of a health/fuel item spawning
    //10% for health
    //10% for fuel
    //10% chance of an ability or passive item spawning
    //7.5% for a passive item
    //2.5% for an ability
    //1% for a rare candy to spawn

    public GameObject spawnedItem;
    public int storedID = -1;

    // Start is called before the first frame update
    void Start()
    {
        itemAtlas = GameObject.FindGameObjectWithTag("Atlas (Item)").GetComponent<ItemAtlas>();

        for (int i = 0; i < itemAtlas.piAtlas.Length; ++i)
        {
            passiveItemDeck.Add(i);
        }
    }

    public void RollForDrops()
    {
        int result = Choose(chances);

        switch(result)
        {
            //Spawn nothing
            case 0:
                Debug.Log("Nothing Rolled");
                break;

            //spawn a health or fuel pickup
            case 1:
                SpawnHealth();
                break;

            //spawn a passive item or ability
            case 2:
                SpawnPassiveAndAbilities();
                break;

            //spawn a rare candy
            case 3:
                SpawnCandy();
                break;
        }
    }

    //straight from the unity documentation
    int Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    void SpawnHealth()
    {
        int result = Random.Range(1, 2);

        if (result == 1) //hearts
        {
            result = Random.Range(1, 2);

            if(result == 1) //half heart
            {
                spawnedItem = Instantiate(itemAtlas.puAtlas[1]);
                Debug.Log("Half heart rolled");
            }
            else //full heart
            {
                spawnedItem = Instantiate(itemAtlas.puAtlas[2]);
                Debug.Log("Full heart rolled");
            }
        }
        else //fuel
        {
            spawnedItem = Instantiate(itemAtlas.puAtlas[3]);
            Debug.Log("Fuel rolled)");
        }
    }

    void SpawnPassiveAndAbilities()
    {
        int result = Random.Range(1, 4);

        if(result == 4) //ability
        {
            result = Random.Range(0, itemAtlas.abAtlas.Length);

            spawnedItem = Instantiate(itemAtlas.abAtlas[result]);
            Debug.Log("Ability of ID:" + result + " rolled");
        }
        else //passive
        {
            if (passiveItemDeck.Count == 0) //empty deck, spawn candy
            {
                Debug.Log("Deck empty");
                SpawnCandy();
            }
            else //deck isn't empty, draw from it
            {
                result = Random.Range(0, passiveItemDeck.Count - 1);

                spawnedItem = Instantiate(itemAtlas.piAtlas[result]);
                storedID = result;

                Debug.Log("Passive item of ID:" + result + " rolled");
            }
        }
    }

    void SpawnCandy()
    {
        spawnedItem = Instantiate(itemAtlas.puAtlas[0]);
        Debug.Log("Candy rolled");
    }

    public void DespawnItem()
    {
        if(spawnedItem != null)
        {
            if(storedID != -1) //return uncollected passive item back to the deck
            {
                passiveItemDeck.Add(storedID);
                storedID = -1;
            }

            Destroy(spawnedItem);
        }
    }
}
