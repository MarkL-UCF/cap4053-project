using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WavesReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        //Note: if the file path changes, you need to edit it here
        //Also note: the file can not be open in any other software when running the game (ex: Excel)
        StreamReader sr = new StreamReader("Data/waves.csv");
        bool EOF = false;
        while (!EOF)
        {
            string line = sr.ReadLine();
            if (line == null)
            {
                EOF = true;
                break;
            }
            var vals = line.Split(',');
            //get data here

            Debug.Log("value read: " + vals[0]);
        }
    }
}
