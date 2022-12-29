using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{    
    [SerializeField]
    private int health = 100;
    [SerializeField]
    public int speed = 300;

    [SerializeField]
    public int chaseDistance;

    [SerializeField]
    private GameObject deathEffect;    
    private float dazedTime;
    [SerializeField]
    private float StartDazeTime;

    [SerializeField]
    private SimpleFlash simpleFlash;    
    private int _speedy;

    private void Start()
    {
        _speedy = speed;
    }

    private void Update()
    {
        if(dazedTime <= 0)
        {
            speed = _speedy;
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
}
