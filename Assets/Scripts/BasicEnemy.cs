using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{    
    [SerializeField]
    private int health = 100;
    [SerializeField]
    public int speed = 5;

    [SerializeField]
    private GameObject deathEffect;    
    private float dazedTime;
    [SerializeField]
    private float StartDazeTime;

    [SerializeField]
    private SimpleFlash simpleFlash;

    [SerializeField]
    private int DamageToPlayer = 5;

    [SerializeField]
    public PlayerStatistic playerStatistic;

    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 5;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
    }

    public void TakeDamage (int damage)
    {
        if (health < 0)
        {           
            StartCoroutine(Die());            
        }
        else
        {
            health -= damage;
            simpleFlash.Flash();
        }
    }

    IEnumerator Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<Renderer>().material.color = Color.red;        
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            _rigidBody2D.velocity = new Vector2(0f, 0f);
            playerStatistic.TakeDamage(DamageToPlayer);
        }
    }
}
