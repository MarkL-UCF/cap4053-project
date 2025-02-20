using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyEnemyScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject); //immediately self destruct
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
