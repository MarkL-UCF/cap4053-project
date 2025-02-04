using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //Get the mouse position
        Vector3 aimRotation = mousePos - transform.position; //Get rotation vector

        float rotZ = Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg; // Calculate angle

        transform.rotation = Quaternion.Euler(0, 0, rotZ); //Perform the rotation

    }
}
