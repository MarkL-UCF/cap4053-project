using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;

public class Metrics : MonoBehaviour
{
    private float creationTime;
    private string filePath = "Data/metrics.txt";
    private int roomCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
        startRun();
    }

    public void startRun()
    {
        if (File.Exists((filePath)))
        {
            File.AppendAllText(filePath, "\n* * * * * NEW RUN STARTED * * * * *\n");
        }
        else //create a text file
        {
            File.WriteAllText(filePath, "\n* * * * * NEW RUN STARTED * * * * *\n");
        }
    }

    public void newRoom()
    {
        File.AppendAllText(filePath, "[" + (Time.time - creationTime) + "s] Room encounter #" + roomCounter + " started\n");
    }

    public void foundItem(int itemID)
    {
        File.AppendAllText(filePath, "[" + (Time.time - creationTime) + "s] Item spawned with ID #" + itemID + "\n");
    }

    public void playerDied()
    {
        File.AppendAllText(filePath, "[" + (Time.time - creationTime) + "s] Player has died\n");
    }

    public void playerWon()
    {
        File.AppendAllText(filePath, "[" + (Time.time - creationTime) + "s] Player has won\n");
    }
}
