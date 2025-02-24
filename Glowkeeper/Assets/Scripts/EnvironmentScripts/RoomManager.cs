using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;

    private GameObject roomManager;

    public GameObject flamePrefab;
    public GameObject flame;
    public GameObject flameAnchor;

    public List<GameObject> activeEnemies = new List<GameObject>();

    private BoxCollider2D trigger;


    public EnemyAtlas enemyAtlas;
    public WaveAtlas waveAtlas;
    public flameHealth flameHP;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = gameObject;
        enemyAtlas = GameObject.FindGameObjectWithTag("Atlas (Enemy)").GetComponent <EnemyAtlas>();
        waveAtlas = GameObject.FindGameObjectWithTag("Atlas (Wave)").GetComponent<WaveAtlas>();
        flameAnchor = roomManager.transform.Find("Flame Anchor").gameObject;
        trigger = this.GetComponent<BoxCollider2D>();
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
            trigger.enabled = false; //remove collider for runtime efficiency
            StartCoroutine(DoEncounter());
        }
    }

    //Perform the actual encounter
    IEnumerator DoEncounter()
    {
        flame = Instantiate(flamePrefab, flameAnchor.transform.position, flameAnchor.transform.rotation);
        //to do: set this script as the room manager in the instantiated flame

        yield return new WaitForSeconds(3);
        Debug.Log("3 seconds have passed, starting encounter now...");

        int waveID = Random.Range(0, waveAtlas.f1PoolSize);
        Debug.Log("Selected Wave ID: " + waveID);

        /*
        GameObject InstantiatedEnemy1 = Instantiate(enemyAtlas.eAtlas[1], spawn1.transform.position, spawn1.transform.rotation);
        GameObject InstantiatedEnemy2 = Instantiate(enemyAtlas.eAtlas[1], spawn2.transform.position, spawn2.transform.rotation);
        GameObject InstantiatedEnemy3 = Instantiate(enemyAtlas.eAtlas[0], spawn3.transform.position, spawn3.transform.rotation);
        GameObject InstantiatedEnemy4 = Instantiate(enemyAtlas.eAtlas[2], spawn4.transform.position, spawn4.transform.rotation);
        */

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

        Debug.Log("Room Complete");


    }

}
