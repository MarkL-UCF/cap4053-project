using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

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
            topWall.SetActive(true);  // Ensure wall is deactivated when door is open
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
            bottomWall.SetActive(true);
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
            leftWall.SetActive(true);
        }

        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
            rightWall.SetActive(true);
        }
    }

    // Activates walls if doors did not activate
    public void ActivateWallsWithoutDoors()
    {
        // Activate walls only if corresponding doors are not active
        if (!topDoor.activeSelf) topWall.SetActive(false);
        if (!bottomDoor.activeSelf) bottomWall.SetActive(false);
        if (!leftDoor.activeSelf) leftWall.SetActive(false);
        if (!rightDoor.activeSelf) rightWall.SetActive(false);
    }
}

