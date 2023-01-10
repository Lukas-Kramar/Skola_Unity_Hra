using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public int throwForce = 40;
    public GameObject grenadePrefab;
    private PlayerStatistic playerStatistic;
    [SerializeField]
    private Transform grenadeLaunch;

    private void Start()
    {
        playerStatistic = GetComponent<PlayerStatistic>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && playerStatistic.numberOfGrenades > 0)
        {
            playerStatistic.numberOfGrenades--;
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
       GameObject grenade = Instantiate(grenadePrefab, grenadeLaunch.position, transform.rotation);
       Rigidbody2D rigidbody2D = grenade.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * throwForce * Time.deltaTime);
    }
}
