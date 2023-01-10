using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Basic_Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePointTop;
    public Transform firePointTopSide;   
    public Transform firePointDown;
    public GameObject bulletPrefab;
    public GameObject UpBulletPrefab;
    public GameObject UpRightBulletPrefab;
    public GameObject UpLeftBulletPrefab;
    public GameObject DownBulletPrefab;    
    public int bulletSpeed;
    public int bulletDMG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) ShootLeftUp();
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) ShootRightUp();
            else if (Input.GetKey(KeyCode.W)) ShootUp();
            else if (Input.GetKey(KeyCode.S)) ShootDown();
            else Shoot();
        }
                
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    void ShootUp()
    {
        Instantiate(UpBulletPrefab, firePointTop.position, firePointTop.rotation);
    }
    private void ShootRightUp()
    {
        Instantiate(UpRightBulletPrefab, firePointTopSide.position, firePointTop.rotation);
    }

    private void ShootLeftUp()
    {
        Instantiate(UpLeftBulletPrefab, firePointTopSide.position, firePointTop.rotation);
    }
    void ShootDown()
    {
        Instantiate(DownBulletPrefab, firePointDown.position, firePointTop.rotation);
    }
}
