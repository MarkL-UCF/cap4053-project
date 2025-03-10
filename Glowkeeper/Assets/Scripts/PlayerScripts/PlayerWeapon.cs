using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    public GameObject bullet;
    public Transform BulletOrigin;
    private float lastShotTime = 0f;
    public float baseDamage = 2;
    public float baseFirerate = 1;
    public int baseProjectiles = 1;
    public float baseSpread = 5;
    public float baseProjectileSpeed = 5;
    public float baseProjectileSize = 1;

    public float damage;
    public float firerate;
    public int numProjectiles;
    public float spread;
    public float projectileSpeed;
    public float projectileSize;

    public float damageFlat = 0;
    public float firerateFlat = 0;
    public int numProjectilesFlat = 0;
    public float spreadFlat = 0;
    public float projectileSpeedFlat = 0;
    public float projectileSizeFlat = 0;

    public float damageScalar = 1;
    public float firerateScalar = 1;
    public float spreadScalar = 1;
    public float projectileSpeedScalar = 1;
    public float projectileSizeScalar = 1;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //Get the mouse position

        //Left click to fire, check that enough time has passed since last
        if (Input.GetMouseButton(0) && Time.time > lastShotTime + firerate)
        {
            Fire();
        }
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

    //Runs stat calculations, call whenever the stats change
    public void UpdateStats() {
        damage = Mathf.Max((baseDamage + damageFlat) * damageScalar, .5f);
        firerate = Mathf.Clamp((baseFirerate + firerateFlat) * firerateScalar, .05f, 10);
        numProjectiles = Mathf.Max((baseProjectiles + numProjectilesFlat), 1);
        spread = Mathf.Clamp((baseSpread + spreadFlat) * spreadScalar, 0, 180);
        projectileSpeed = Mathf.Clamp((baseProjectileSpeed + projectileSpeedFlat) * projectileSpeedScalar, 0.5f, 15);
        projectileSize = Mathf.Clamp((baseProjectileSize + projectileSizeFlat) * projectileSizeScalar, 0.1f, 3);
    }
}

