using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    public GameObject bullet;
    public Transform BulletOrigin;
    public bool canFire;
    private float timer;
    public float firerate = 1;
    public float projectileSpeed = 3;

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

        //Left click to fire
        if (Input.GetMouseButton(0))
            Fire();
    }

    //Handles firing the weapon
    void Fire()
    {
        //Reset canFire if enough time has passed
        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > firerate)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(canFire == true) //Only fire if off cooldown
        {
            canFire = false;

            //Create a bullet object
            GameObject InstantiatedBullet = Instantiate(bullet, BulletOrigin.position, Quaternion.identity);

            //Pass on needed values to the bullet's script
            var bulletScript = InstantiatedBullet.GetComponent<PlayerProjectile>();
            bulletScript.mousePos = mousePos;
            bulletScript.projectileSpeed = projectileSpeed;
        }
    }
}
