using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour

{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject startRoomPrefab;
    [SerializeField] GameObject endRoomPrefab;


    [SerializeField] private int maxRooms = 20;
    [SerializeField] private int minRooms = 18;

    [SerializeField] int roomWidth = 15;
    [SerializeField] int roomHeight = 10;

    [SerializeField] int gridSizeX = 4;
    [SerializeField] int gridSizeY = 4;

    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;
    private int roomCount;
    private bool generationComplete = false;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("roomCount was less than the minimum amount of rooms. trying again");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created");
            ActivateWallsForAllRooms();
            ConnectAllTeleporters();
            generationComplete = true;
        }
    }


void ConnectTeleporters(Room roomA, Room roomB, Vector2Int direction)
{
    if (roomA == null || roomB == null) return;

    GameObject doorA = roomA.GetDoor(direction);
    GameObject doorB = roomB.GetDoor(-direction);  // Opposite direction
    GameObject CameraA = roomA.GetCamera(direction);
    GameObject CameraB = roomB.GetCamera(-direction);  // Opposite direction

    if (doorA != null && doorB != null)
    {
        DoorTeleporter teleA = doorA.GetComponent<DoorTeleporter>();
        DoorTeleporter teleB = doorB.GetComponent<DoorTeleporter>();

        if (teleA != null && teleB != null)
        {
            teleA.connectedTeleportSpot = doorB;
            teleB.connectedTeleportSpot = doorA;
        }
    }
        if (CameraA != null && CameraB != null)
    {
        DoorTeleporter teleA = doorA.GetComponent<DoorTeleporter>();
        DoorTeleporter teleB = doorB.GetComponent<DoorTeleporter>();

        if (teleA != null && teleB != null)
        {
            teleA.CameraAnchor = CameraB;
            teleB.CameraAnchor = CameraA;
        }
    }
}

    void ConnectAllTeleporters()
{
    foreach (GameObject roomObject in roomObjects)
    {
        Room room = roomObject.GetComponent<Room>();
        Vector2Int roomIndex = room.RoomIndex;

        Room leftRoom = GetRoomScriptAt(new Vector2Int(roomIndex.x - 1, roomIndex.y));
        Room rightRoom = GetRoomScriptAt(new Vector2Int(roomIndex.x + 1, roomIndex.y));
        Room topRoom = GetRoomScriptAt(new Vector2Int(roomIndex.x, roomIndex.y + 1));
        Room bottomRoom = GetRoomScriptAt(new Vector2Int(roomIndex.x, roomIndex.y - 1));

        if (leftRoom != null) ConnectTeleporters(room, leftRoom, Vector2Int.left);
        if (rightRoom != null) ConnectTeleporters(room, rightRoom, Vector2Int.right);
        if (topRoom != null) ConnectTeleporters(room, topRoom, Vector2Int.up);
        if (bottomRoom != null) ConnectTeleporters(room, bottomRoom, Vector2Int.down);
    }

    Debug.Log("[DungeonManager] Finished connecting all teleporters!");
}


    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(startRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;
        if (roomGrid[x, y] != 0)
            return false;


        if (roomCount >= maxRooms)
            return false;
        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = gameObject;
        if(roomCount < maxRooms)
            newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        else if(roomCount ==maxRooms)
            newRoom = Instantiate(endRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);

            
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);
        


        return true;
    }

    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

void OpenDoors(GameObject room, int x, int y)
{
    Room newRoomScript = room.GetComponent<Room>();

    Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
    Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
    Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
    Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

    if (x > 0 && roomGrid[x - 1, y] != 0)
    {
        newRoomScript.OpenDoor(Vector2Int.left);
        leftRoomScript?.OpenDoor(Vector2Int.right);
        ConnectTeleporters(newRoomScript, leftRoomScript, Vector2Int.left);
    }
    if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
    {
        newRoomScript.OpenDoor(Vector2Int.right);
        rightRoomScript?.OpenDoor(Vector2Int.left);
        ConnectTeleporters(newRoomScript, rightRoomScript, Vector2Int.right);
    }
    if (y > 0 && roomGrid[x, y - 1] != 0)
    {
        newRoomScript.OpenDoor(Vector2Int.down);
        bottomRoomScript?.OpenDoor(Vector2Int.up);
        ConnectTeleporters(newRoomScript, bottomRoomScript, Vector2Int.down);
    }
    if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
    {
        newRoomScript.OpenDoor(Vector2Int.up);
        topRoomScript?.OpenDoor(Vector2Int.down);
        ConnectTeleporters(newRoomScript, topRoomScript, Vector2Int.up);
    }
}




    void ActivateWallsForAllRooms()
    {
        // After all doors have been set, activate walls where no doors are present
        foreach (GameObject roomObject in roomObjects)
        {
            Room roomScript = roomObject.GetComponent<Room>();
            roomScript.ActivateWallsWithoutDoors();
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++;

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}

