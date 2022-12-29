using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private Player _player;
    private PlayerStatistic _statistic;
    private void Start()
    {
        _player = GetComponent<Player>();
        _statistic = GetComponent<PlayerStatistic>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Console.WriteLine("Trigger Collision: " + collision.gameObject.tag);
        if (collision.CompareTag("Meds"))
        {
            Destroy(collision.gameObject);
            _statistic.Heal(30);
        }
    }

}
