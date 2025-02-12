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

    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(!hasTriggered)
        {
            Debug.Log("Encounter triggered");
            hasTriggered = true;
            StartCoroutine(DoEncounter());
        }
    }

    IEnumerator DoEncounter()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("3 seconds have passed, starting encounter now...");
    }
}
