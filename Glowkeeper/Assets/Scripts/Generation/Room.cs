using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public GameObject topDoor;
    [SerializeField] public GameObject bottomDoor;
    [SerializeField] public GameObject leftDoor;
    [SerializeField] public GameObject rightDoor;

    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    public Vector2Int RoomIndex { get; set; }

    // Activates doors when applicable
    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
            topWall.SetActive(false);  // Ensure wall is deactivated when door is open
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
            bottomWall.SetActive(false);
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
            leftWall.SetActive(false);
        }

        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
            rightWall.SetActive(false);
        }



    }
public GameObject GetDoor(Vector2Int direction)
{
    GameObject doorParent = null;

    if (direction == Vector2Int.up) doorParent = topDoor;
    if (direction == Vector2Int.down) doorParent = bottomDoor;
    if (direction == Vector2Int.left) doorParent = leftDoor;
    if (direction == Vector2Int.right) doorParent = rightDoor;

    if (doorParent != null)
    {
        // This issue is that the script is using itself and not the doorParent object so only looks for the child if the script is attached 
        Transform TeleportSpot = doorParent.transform.Find("Active Door"); // Find the child object

        if (TeleportSpot != null)
        {
            return TeleportSpot.gameObject; // Return Active Door instead of the parent
        }
    }

    return null;
}
    public GameObject GetTeleporter(Vector2Int direction)
    {
        GameObject doorParent = null;

        // Set door parent based on direction
        if (direction == Vector2Int.up) doorParent = topDoor;
        if (direction == Vector2Int.down) doorParent = bottomDoor;
        if (direction == Vector2Int.left) doorParent = leftDoor;
        if (direction == Vector2Int.right) doorParent = rightDoor;

        if (doorParent != null)
        {
            // Try to find the TeleportSpot under the door
            Transform teleportSpot = doorParent.transform.Find("TeleportSpot");

            if (teleportSpot != null)
            {
                return teleportSpot.gameObject; // Return the TeleportSpot game object
            }
            else
            {
                Debug.LogWarning($"TeleportSpot not found under {doorParent.name} in {gameObject.name}");
            }
        }

        return null; // Return null if no TeleportSpot is found
    }



public GameObject GetCamera(Vector2Int direction)
{
    Transform cameraAnchor = transform.Find("Anchors/Camera Anchor");
    return cameraAnchor != null ? cameraAnchor.gameObject : null;

}



    // Activates walls if doors did not activate
    public void ActivateWallsWithoutDoors()
    {
        // Activate walls only if corresponding doors are not active
        //if (!topDoor.activeSelf) topWall.SetActive(true);
        //if (!bottomDoor.activeSelf) bottomWall.SetActive(true);
        //if (!leftDoor.activeSelf) leftWall.SetActive(true);
        //if (!rightDoor.activeSelf) rightWall.SetActive(true);
    //         Debug.Log($"Top Wall Active: {topWall?.activeSelf}");
    // Debug.Log($"Bottom Wall Active: {bottomWall?.activeSelf}");
    // Debug.Log($"Left Wall Active: {leftWall?.activeSelf}");
    // Debug.Log($"Right Wall Active: {rightWall?.activeSelf}");
        if (topWall.activeSelf)
            topDoor.SetActive(false);

        if (bottomWall.activeSelf)
         bottomDoor.SetActive(false);

     if (leftWall.activeSelf)
         leftDoor.SetActive(false);

     if (rightWall.activeSelf)
         rightDoor.SetActive(false);
    // Debug.Log($"Top Wall Active: {topWall?.activeSelf}");
    // Debug.Log($"Bottom Wall Active: {bottomWall?.activeSelf}");
    // Debug.Log($"Left Wall Active: {leftWall?.activeSelf}");
    // Debug.Log($"Right Wall Active: {rightWall?.activeSelf}");
    Debug.Log($"Top Door Active: {topDoor?.activeSelf}");
    Debug.Log($"Bottom Door Active: {bottomDoor?.activeSelf}");
    Debug.Log($"Left Door Active: {leftDoor?.activeSelf}");
    Debug.Log($"Right Door Active: {rightDoor?.activeSelf}");

    

    
    }

    
}

