using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public PlayerStatistic player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Console.WriteLine(collision.gameObject.tag);
        if(collision.CompareTag("Meds"))
        {
            Destroy(collision.gameObject);
            player.TakeMeds(30);
        }
    }
}
