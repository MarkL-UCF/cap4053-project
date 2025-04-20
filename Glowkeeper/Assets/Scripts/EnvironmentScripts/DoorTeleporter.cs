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
            float yOffset = 0.25f;
            // For 2D: use anchor's x and y, but force z = -10 (or your default cam z)
            Vector3 targetPos = new Vector3(anchorPos.x, anchorPos.y + yOffset , -10f);
            MainCamera.transform.position = targetPos;

            collision.gameObject.transform.SetPositionAndRotation(connectedTeleportSpot.transform.position, collision.gameObject.transform.rotation); //teleport player
            //itemSpawner.GetComponent<ItemSpawner>().DespawnItem();
            itemSpawner.transform.SetPositionAndRotation(ItemAnchor.transform.position, ItemAnchor.transform.rotation);

        }
    }
}

