using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public PlayerItems CurrentItem;
    public Boolean newPickup;
    public float StartFirerate;
    public float StartNumProjectiles;
    public float StartSpread;
    public float StartProjectileSpeed;
    public float StartProjectileSize;

    private void Start()
    {
        //Store Base Stat Values
        var playerWeapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
        StartFirerate = playerWeapon.firerate;
        StartNumProjectiles = playerWeapon.numProjectiles;
        StartSpread = playerWeapon.spread;
        StartProjectileSpeed = playerWeapon.projectileSpeed;
        StartProjectileSize = playerWeapon.projectileSize;

        newPickup = false;
}
    // Update is called once per frame
    void Update()
    {
        if (newPickup)
        {
            //reset stats before applying new item stats
            var playerWeapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerWeapon>();
            playerWeapon.firerate = StartFirerate;
            playerWeapon.numProjectiles = StartNumProjectiles;
            playerWeapon.spread = StartSpread;
            playerWeapon.projectileSpeed = StartProjectileSpeed;
            playerWeapon.projectileSize = StartProjectileSize;

            CurrentItem.Activate(gameObject);

            newPickup = false;
        }
    }
}
