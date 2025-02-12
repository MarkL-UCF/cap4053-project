using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    private BoxCollider2D trigger;

    public EnemyAtlas enemyAtlas;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        enemyAtlas = GameObject.FindGameObjectWithTag("Atlas (Enemy)").GetComponent <EnemyAtlas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check for collision to start the encounter
    private void OnTriggerEnter2D(Collider2D trigger)
    {
            Debug.Log("Encounter triggered");
            trigger.enabled = false; //remove collider for runtime efficiency
            StartCoroutine(DoEncounter());
    }

    //Perform the actual encounter
    IEnumerator DoEncounter()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("3 seconds have passed, starting encounter now...");

        GameObject InstantiatedEnemy1 = Instantiate(enemyAtlas.eAtlas[0], spawn1.transform.position, spawn1.transform.rotation);
        GameObject InstantiatedEnemy1 = Instantiate(enemyAtlas.eAtlas[0], spawn1.transform.position, spawn1.transform.rotation);
    }
}
