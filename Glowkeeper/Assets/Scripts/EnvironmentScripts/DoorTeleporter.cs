using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleporter : MonoBehaviour
{
    public GameObject connectedTeleportSpot;
    public GameObject MainCamera;
    public GameObject CameraAnchor;
    public GameObject itemSpawner;
    public GameObject ItemAnchor;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        itemSpawner = GameObject.FindGameObjectWithTag("Item Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.SetPositionAndRotation(connectedTeleportSpot.transform.position, collision.gameObject.transform.rotation); //teleport player
            itemSpawner.GetComponent<ItemSpawner>().DespawnItem();
            itemSpawner.transform.SetPositionAndRotation(ItemAnchor.transform.position, ItemAnchor.transform.rotation);
            MainCamera.transform.SetPositionAndRotation(CameraAnchor.transform.position, CameraAnchor.transform.rotation);
        }
    }
}
