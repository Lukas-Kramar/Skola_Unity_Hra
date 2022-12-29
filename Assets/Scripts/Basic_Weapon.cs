using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Basic_Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePointTop;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Input.GetKeyDown("W")) ShootUp();
            else Shoot();
        }
                
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    void ShootUp()
    {
        Instantiate(bulletPrefab, firePointTop.position, firePointTop.rotation);
    }
}
