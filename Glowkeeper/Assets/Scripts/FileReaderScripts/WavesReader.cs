using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WavesReader : MonoBehaviour
{
    WaveAtlas atlas;

    // Start is called before the first frame update
    void Start()
    {
        atlas = GameObject.FindGameObjectWithTag("Atlas (Wave)").GetComponent<WaveAtlas>();
        ReadCSV();
    }

    void ReadCSV()
    {
        //Note: if the file path changes, you need to edit it here
        //Also note: DO NOT EDIT THE CSV WITH EXCEL, IT WILL BREAK THE FORMATTING
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
            

            //format of waves.csv

            //cell A: Floor where wave can occur
            //cells B-G: Wave 1 Enemy IDs
            //cells H-M: Wave 2 Enemy IDs
            //cells N-S: Wave 3 Enemy IDs

            //Reminder that an ID of 0 means no enemy will spawn

            if (int.Parse(vals[0]) == 1) //Floor one wave
            {
                atlas.f1w1s1.Add(int.Parse(vals[1]));
                atlas.f1w1s2.Add(int.Parse(vals[2]));
                atlas.f1w1s3.Add(int.Parse(vals[3]));
                atlas.f1w1s4.Add(int.Parse(vals[4]));
                atlas.f1w1s5.Add(int.Parse(vals[5]));
                atlas.f1w1s6.Add(int.Parse(vals[6]));

                atlas.f1w2s1.Add(int.Parse(vals[7]));
                atlas.f1w2s2.Add(int.Parse(vals[8]));
                atlas.f1w2s3.Add(int.Parse(vals[9]));
                atlas.f1w2s4.Add(int.Parse(vals[10]));
                atlas.f1w2s5.Add(int.Parse(vals[11]));
                atlas.f1w2s6.Add(int.Parse(vals[12]));

                atlas.f1w3s1.Add(int.Parse(vals[13]));
                atlas.f1w3s2.Add(int.Parse(vals[14]));
                atlas.f1w3s3.Add(int.Parse(vals[15]));
                atlas.f1w3s4.Add(int.Parse(vals[16]));
                atlas.f1w3s5.Add(int.Parse(vals[17]));
                atlas.f1w3s6.Add(int.Parse(vals[18]));

                atlas.f1PoolSize++;
            }
        }
    }
}
