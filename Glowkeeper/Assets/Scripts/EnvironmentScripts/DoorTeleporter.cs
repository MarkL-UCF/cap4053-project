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
