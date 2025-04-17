using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    public ItemAtlas itemAtlas;

    public ArrayList passiveItemDeck = new ArrayList();

    float[] chances = {.59f, .25f, .15f, .1f};
    //59% chance of coins (previously nothing) spawning
    //25% chance of a health/fuel item spawning
    //12.5% for health
    //12.5% for fuel
    //15% chance of an ability or passive item spawning
    //11.25% for a passive item
    //3.75% for an ability
    //1% for a rare candy to spawn

    float[] coinChances = { .50f, .35f, .15f };

    public GameObject spawnedItem;
    //public int storedID = -1;

    public Metrics metrics;
    // Start is called before the first frame update
    void Start()
    {
        itemAtlas = GameObject.FindGameObjectWithTag("Atlas (Item)").GetComponent<ItemAtlas>();
        metrics = GameObject.FindGameObjectWithTag("Metrics").GetComponent<Metrics>();

        for (int i = 0; i < itemAtlas.piAtlas.Length; ++i)
        {
            passiveItemDeck.Add(i);
        }
    }

    private void Update()
    {
        

    }

    public void RollForDrops()
    {
        int result = Choose(chances);

        switch(result)
        {
            //Spawn nothing
            case 0:
                //Debug.Log("Nothing Rolled");
                SpawnCoin();
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

    void SpawnCoin()
    {
        int result = Choose(coinChances);

        if (result == 0) //1$
        {

            Debug.Log("1$ spawned");
            spawnedItem = Instantiate(itemAtlas.coinAtlas[0], gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (result == 1) //5$
        {
            Debug.Log("5$ spawned");
            spawnedItem = Instantiate(itemAtlas.coinAtlas[1], gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (result == 2) //10$
        {
            Debug.Log("10$ spawned");
            spawnedItem = Instantiate(itemAtlas.coinAtlas[2], gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    void SpawnHealth()
    {
        int result = Random.Range(1, 2);

        if (result == 1) //hearts
        {
            result = Random.Range(1, 2);

            if(result == 1) //half heart
            {
                spawnedItem = Instantiate(itemAtlas.puAtlas[1], gameObject.transform.position, gameObject.transform.rotation);
                Debug.Log("Half heart rolled");
            }
            else //full heart
            {
                spawnedItem = Instantiate(itemAtlas.puAtlas[2], gameObject.transform.position, gameObject.transform.rotation);
                Debug.Log("Full heart rolled");
            }
        }
        else //fuel
        {
            spawnedItem = Instantiate(itemAtlas.puAtlas[3], gameObject.transform.position, gameObject.transform.rotation);
            Debug.Log("Fuel rolled)");
        }
    }

    void SpawnPassiveAndAbilities()
    {
        int result = Random.Range(1, 4);

        if(result == 4) //ability
        {
            result = Random.Range(0, itemAtlas.abAtlas.Length);

            spawnedItem = Instantiate(itemAtlas.abAtlas[result], gameObject.transform.position, gameObject.transform.rotation);

            Debug.Log("Ability of ID:" + result + " rolled");
        }
        else //passive
        {
            if (passiveItemDeck.Count == 0) //empty deck, spawn candy
            {
                Debug.Log("Deck empty");
                SpawnCandy();
                metrics.foundItem(-1);
            }
            else //deck isn't empty, draw from it
            {
                result = Random.Range(0, passiveItemDeck.Count - 1);

                spawnedItem = Instantiate(itemAtlas.piAtlas[result], gameObject.transform.position, gameObject.transform.rotation);

                //storedID = result;

                Debug.Log("Passive item of ID:" + result + " rolled");
                metrics.foundItem(result);
            }
        }
    }

    void SpawnCandy()
    {
        spawnedItem = Instantiate(itemAtlas.puAtlas[0], gameObject.transform.position, gameObject.transform.rotation);
        Debug.Log("Candy rolled");
        metrics.foundItem(-1);
    }

    /*
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
    */

    public void ShopSpawnItem()
    {
        int result = Random.Range(1, 4);
        int candyResult = Random.Range(1, 100);
        if (candyResult <= 95)
        {
            if (result == 4) //ability
            {
                result = Random.Range(0, itemAtlas.abAtlas.Length);

                spawnedItem = Instantiate(itemAtlas.abAtlas[result], gameObject.transform.position, gameObject.transform.rotation);
                spawnedItem.GetComponent<AbilityPickup>().shopItem = true;

                Debug.Log("(Shop) Ability of ID:" + result + " rolled");
            }
            else //passive
            {
                if (passiveItemDeck.Count == 0) //empty deck, spawn candy
                {
                    Debug.Log("(Shop) Deck empty");
                    SpawnCandy();
                    spawnedItem.GetComponent<CandyItem>().shopItem = true;
                    metrics.foundItem(-1);
                }
                else //deck isn't empty, draw from it
                {
                    result = Random.Range(0, passiveItemDeck.Count - 1);

                    spawnedItem = Instantiate(itemAtlas.piAtlas[result], gameObject.transform.position, gameObject.transform.rotation);
                    spawnedItem.GetComponent<WeaponPickup>().shopItem = true;

                    //storedID = result;

                    Debug.Log("(Shop) passive item of ID:" + result + " rolled");
                    metrics.foundItem(result);
                }
            }
        }
        else //5% chance of a candy item replacing an item spawn
        {
            SpawnCandy();
            Debug.Log("(Shop) Candy replaced item");
            spawnedItem.GetComponent<CandyItem>().shopItem = true;
        }
    }

    public void ShopSpawnPickup()
    {
        int result = Random.Range(1, 2);
        int candyResult = Random.Range(1, 100);

        if (candyResult <= 90)
        {
            if (result == 1) //hearts
            {
                result = Random.Range(1, 2);

                if (result == 1) //half heart
                {
                    spawnedItem = Instantiate(itemAtlas.puAtlas[1], gameObject.transform.position, gameObject.transform.rotation);
                    Debug.Log("(Shop) Half heart rolled");
                }
                else //full heart
                {
                    spawnedItem = Instantiate(itemAtlas.puAtlas[2], gameObject.transform.position, gameObject.transform.rotation);
                    Debug.Log("(Shop) Full heart rolled");
                }
            }
            else //fuel
            {
                spawnedItem = Instantiate(itemAtlas.puAtlas[3], gameObject.transform.position, gameObject.transform.rotation);
                Debug.Log("(Shop) Fuel rolled");
            }
        }
        else //10% chance of a candy replacing a shop pickup spawn
        {
            SpawnCandy();
            Debug.Log("(Shop) Candy replaced pickup");
            spawnedItem.GetComponent<CandyItem>().shopItem = true;
        }
    }
}
