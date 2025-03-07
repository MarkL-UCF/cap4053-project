using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    public GameObject bullet;
    public Transform BulletOrigin;
    private float lastShotTime = 0f;
    public int damage = 2;
    public float firerate = 1;
    public float numProjectiles = 1;
    public float spread = 5;
    public float projectileSpeed = 5;
    public float projectileSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //Get the mouse position

        //Left click to fire, check that enough time has passed since last
        if (Input.GetMouseButton(0) && Time.time > lastShotTime + firerate)
            Fire();
    }

    //Handles firing the weapon
    void Fire()
    {

        Vector3 gunPos = transform.position;
        var direction = new Vector2(mousePos.x - gunPos.x, mousePos.y - gunPos.y);
        transform.up = direction;

        //apply bullet spread
        float selectedSpread = Random.Range(-spread, spread);
        BulletOrigin.localRotation = Quaternion.Euler(new Vector3(BulletOrigin.localRotation.x, BulletOrigin.localRotation.y, selectedSpread));
        //Create a bullet object
        for (int i = 0; i < numProjectiles; i++)
        {
            
            if (i != 0)//adjust angle/spread if more than one bullet
            {
                BulletOrigin.localRotation = Quaternion.Euler(new Vector3(BulletOrigin.localRotation.x, BulletOrigin.localRotation.y, selectedSpread + (5*i)));
            }
            //create bullet
            GameObject InstantiatedBullet = Instantiate(bullet, BulletOrigin.position, BulletOrigin.rotation);
            
            //set the projectile's speed
            InstantiatedBullet.GetComponent<Rigidbody2D>().velocity = BulletOrigin.up.normalized * projectileSpeed;

            //Pass on needed values to the bullet's script
            var bulletScript = InstantiatedBullet.GetComponent<PlayerProjectile>();
            bulletScript.projectileSize = projectileSize;
            bulletScript.damage = damage;
            
        }

        //start cooldown
        lastShotTime = Time.time;
    }
}

