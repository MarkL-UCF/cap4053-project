using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class DoorTeleporter : MonoBehaviour
{
    
    public GameObject connectedTeleportSpot;
    public GameObject MainCamera;
    public GameObject CameraAnchor;
    public GameObject itemSpawner;
    public GameObject ItemAnchor;

    private bool playerInTrigger = false;
    private GameObject player;

    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        itemSpawner = GameObject.FindGameObjectWithTag("Item Spawner");
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"[DoorTeleporter] Player entered door: {gameObject.name}");
            playerInTrigger = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false;
            player = null;
        }
    }

    private void TeleportPlayer()
    {
        if (player == null || connectedTeleportSpot == null)
        {
            Debug.LogError($"[DoorTeleporter] Teleport failed! Player or connected spot is missing.");
            return;
        }

        // Teleport player
        player.transform.SetPositionAndRotation(connectedTeleportSpot.transform.position, connectedTeleportSpot.transform.rotation);
        Debug.Log($"[DoorTeleporter] Teleported player to: {connectedTeleportSpot.name}");

        // Move Item Spawner
        if (itemSpawner != null && ItemAnchor != null)
        {
            itemSpawner.GetComponent<ItemSpawner>().DespawnItem();
            itemSpawner.transform.SetPositionAndRotation(ItemAnchor.transform.position, ItemAnchor.transform.rotation);
            Debug.Log($"[DoorTeleporter] ItemSpawner moved to: {ItemAnchor.name}");
        }
        else
        {
            Debug.LogError("[DoorTeleporter] ItemSpawner or ItemAnchor is missing!");
        }

        // Move Camera
        if (MainCamera != null && CameraAnchor != null)
        {
            MainCamera.transform.SetPositionAndRotation(CameraAnchor.transform.position, CameraAnchor.transform.rotation);
            Debug.Log($"[DoorTeleporter] Camera moved to: {CameraAnchor.name}");
        }
        else
        {
            Debug.LogError("[DoorTeleporter] MainCamera or CameraAnchor is missing!");
        }
    }
}
    */


    //Old unbroken code
public class DoorTeleporter : MonoBehaviour
{
    public GameObject connectedTeleportSpot;
    public GameObject MainCamera;
    public GameObject CameraAnchor;
    public GameObject itemSpawner;
    public GameObject ItemAnchor;
    [SerializeField] PolygonCollider2D mapBoundry;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        itemSpawner = GameObject.FindGameObjectWithTag("Item Spawner");
        mapBoundry = GameObject.Find("Walls").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 anchorPos = CameraAnchor.transform.position;

            // For 2D: use anchor's x and y, but force z = -10 (or your default cam z)
            Vector3 targetPos = new Vector3(anchorPos.x, anchorPos.y, -10f);

            Debug.Log(MainCamera);
            Debug.Log(CameraAnchor);

                        Debug.Log("Hello");
            MainCamera.transform.transform.position = targetPos;
            Debug.Log($"[DoorTeleporter] Camera moved to: {CameraAnchor.name}");


            collision.gameObject.transform.SetPositionAndRotation(connectedTeleportSpot.transform.position, collision.gameObject.transform.rotation); //teleport player
            Debug.Log($"[DoorTeleporter] Teleported player to: {connectedTeleportSpot.name}");
            itemSpawner.GetComponent<ItemSpawner>().DespawnItem();
            itemSpawner.transform.SetPositionAndRotation(ItemAnchor.transform.position, ItemAnchor.transform.rotation);
            Debug.Log("Hello");
                        Debug.Log("Hello");
                                    Debug.Log("Hello");
                                                Debug.Log("Hello");
                                                            Debug.Log("Hello");
            MainCamera.transform.SetPositionAndRotation(CameraAnchor.transform.position, CameraAnchor.transform.rotation);
            Debug.Log($"[DoorTeleporter] Camera moved to: {CameraAnchor.name}");


        }
    }
}

