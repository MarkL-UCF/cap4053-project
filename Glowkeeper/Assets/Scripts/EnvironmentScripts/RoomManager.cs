using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;

    public GameObject flamePrefab;
    public GameObject flame;
    public GameObject flameAnchor;

    public GameObject warnIcon;

    public List<GameObject> doors = new List<GameObject>();

    public List<GameObject> activeEnemies = new List<GameObject>();
    public List<GameObject> activeWarnings = new List<GameObject>();

    private BoxCollider2D trigger;


    public EnemyAtlas enemyAtlas;
    public WaveAtlas waveAtlas;
    public flameHealth flameHP;
    public FlameStatTracker flameStatTracker;
    public ItemSpawner itemSpawner;

    public Metrics metrics;

    // Start is called before the first frame update
    void Start()
    {
        enemyAtlas = GameObject.FindGameObjectWithTag("Atlas (Enemy)").GetComponent <EnemyAtlas>();
        waveAtlas = GameObject.FindGameObjectWithTag("Atlas (Wave)").GetComponent<WaveAtlas>();
        flameStatTracker = GameObject.FindGameObjectWithTag("Global Stat Tracker (Flame)").GetComponent<FlameStatTracker>();
        flameAnchor = gameObject.transform.Find("Anchors").transform.Find("Flame Anchor").gameObject;
        trigger = this.GetComponent<BoxCollider2D>();

        metrics = GameObject.FindGameObjectWithTag("Metrics").GetComponent<Metrics>();

        itemSpawner = GameObject.FindGameObjectWithTag("Item Spawner").GetComponent<ItemSpawner>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check for collision to start the encounter
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Debug.Log("Encounter triggered");
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //remove collider for runtime efficiency
            StartCoroutine(DoEncounter());
        }
    }
    public void UnlockAllDoors()
{
    foreach (GameObject door in doors)
    {
        DoorEncounterWatcher doorScript = door.GetComponent<DoorEncounterWatcher>();
        if (doorScript != null)
        {
            doorScript.UnlockDoor();
        }
    }
}

    //Perform the actual encounter
    IEnumerator DoEncounter()
    {
        yield return null; 

        metrics.newRoom(); //print to metrics file

        //lock all doors

        Room roomscript = GetComponentInParent<Room>();
        roomscript.LockAllDoors();
        

        //instantiate flame if it has not been extinguished
        if(flameStatTracker.isExtinguished == false)
        {
            flame = Instantiate(flamePrefab, flameAnchor.transform.position, flameAnchor.transform.rotation);
            flameHP = flame.GetComponent<flameHealth>();
            flameHP.roomManager = this;

            yield return null; //force the flame to fully instantiate before calling anything on it
            yield return null; //doubled the amount of frames to wait to fix it again

            flameHP.ChangeLights(); //update light level based on previous values
        }

        int waveID = Random.Range(0, waveAtlas.f1PoolSize);
        Debug.Log("Selected Wave ID: " + waveID);
        GameObject warn;

        if ((int)waveAtlas.f1w1s1[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn1.transform.position, spawn1.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w1s2[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn2.transform.position, spawn2.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w1s3[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn3.transform.position, spawn3.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w1s4[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn4.transform.position, spawn4.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w1s5[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn5.transform.position, spawn5.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w1s6[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn6.transform.position, spawn6.transform.rotation);
            activeWarnings.Add(warn);
        }


        if(activeWarnings.Count > 0)
            yield return new WaitForSeconds(3);
        //Debug.Log("3 seconds have passed, starting encounter now...");

        foreach (GameObject warning in activeWarnings)
        {
            Destroy(warning);
        }
        

        

        GameObject enemy; 
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w1s1[waveID]], spawn1.transform.position, spawn1.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w1s2[waveID]], spawn2.transform.position, spawn2.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w1s3[waveID]], spawn3.transform.position, spawn3.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w1s4[waveID]], spawn4.transform.position, spawn4.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w1s5[waveID]], spawn5.transform.position, spawn5.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w1s6[waveID]], spawn6.transform.position, spawn6.transform.rotation);
        activeEnemies.Add(enemy);

        //wait for the enemies to be defeated
        while (activeEnemies.Count > 0)
        {
            //check for null to see if enemy is destroyed
            activeEnemies.RemoveAll(enemy => enemy == null);

            //wait a frame before checking again
            yield return null;
        }

        Debug.Log("Enemies cleared, spawning second wave...");
        if ((int)waveAtlas.f1w2s1[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn1.transform.position, spawn1.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w2s2[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn2.transform.position, spawn2.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w2s3[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn3.transform.position, spawn3.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w2s4[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn4.transform.position, spawn4.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w2s5[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn5.transform.position, spawn5.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w2s6[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn6.transform.position, spawn6.transform.rotation);
            activeWarnings.Add(warn);
        }

        if (activeWarnings.Count > 0)
            yield return new WaitForSeconds(3);

        foreach (GameObject warning in activeWarnings)
        {
            Destroy(warning);
        }

        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w2s1[waveID]], spawn1.transform.position, spawn1.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w2s2[waveID]], spawn2.transform.position, spawn2.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w2s3[waveID]], spawn3.transform.position, spawn3.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w2s4[waveID]], spawn4.transform.position, spawn4.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w2s5[waveID]], spawn5.transform.position, spawn5.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w2s6[waveID]], spawn6.transform.position, spawn6.transform.rotation);
        activeEnemies.Add(enemy);

        //wait for the enemies to be defeated
        while (activeEnemies.Count > 0)
        {
            //check for null to see if enemy is destroyed
            activeEnemies.RemoveAll(enemy => enemy == null);

            //wait a frame before checking again
            yield return null;
        }

        Debug.Log("Enemies cleared, spawning final wave...");

        if ((int)waveAtlas.f1w3s1[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn1.transform.position, spawn1.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w3s2[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn2.transform.position, spawn2.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w3s3[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn3.transform.position, spawn3.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w3s4[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn4.transform.position, spawn4.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w3s5[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn5.transform.position, spawn5.transform.rotation);
            activeWarnings.Add(warn);
        }
        if ((int)waveAtlas.f1w3s6[waveID] != 0)
        {
            warn = Instantiate(warnIcon, spawn6.transform.position, spawn6.transform.rotation);
            activeWarnings.Add(warn);
        }

        if (activeWarnings.Count > 0)
            yield return new WaitForSeconds(3);

        foreach (GameObject warning in activeWarnings)
        {
            Destroy(warning);
        }

        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w3s1[waveID]], spawn1.transform.position, spawn1.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w3s2[waveID]], spawn2.transform.position, spawn2.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w3s3[waveID]], spawn3.transform.position, spawn3.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w3s4[waveID]], spawn4.transform.position, spawn4.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w3s5[waveID]], spawn5.transform.position, spawn5.transform.rotation);
        activeEnemies.Add(enemy);
        enemy = Instantiate(enemyAtlas.eAtlas[(int)waveAtlas.f1w3s6[waveID]], spawn6.transform.position, spawn6.transform.rotation);
        activeEnemies.Add(enemy);

        //wait for the enemies to be defeated
        while (activeEnemies.Count > 0)
        {
            //check for null to see if enemy is destroyed
            activeEnemies.RemoveAll(enemy => enemy == null);

            //wait a frame before checking again
            yield return null;
        }

        if(flameStatTracker.isExtinguished == false)
        {
            flameHP.DespawnFlame();
        }
        

        //unlock all doors

        Room doorScript =  GetComponentInParent<Room>();
        doorScript.UnlockAllDoors();
        

        //roll for a drop
        itemSpawner.GetComponent<ItemSpawner>().RollForDrops();

        Debug.Log("Room Complete");

        
    }

}
