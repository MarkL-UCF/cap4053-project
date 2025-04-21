using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEncounterWatcher : MonoBehaviour
{
    GameObject ActiveDoor;

    // Start is called before the first frame update
    void Start()
    {
        ActiveDoor = gameObject.transform.Find("Active Door").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockDoor()
    {
        try {ActiveDoor.SetActive(false);} catch {}
    }

    public void UnlockDoor()
    {
        ActiveDoor.SetActive(true);
    }
}
