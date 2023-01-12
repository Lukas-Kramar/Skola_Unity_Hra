using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    [SerializeField]
    public int speed = 300;

    [SerializeField]
    public int chaseDistance;
    
    private float dazedTime;
    [SerializeField]
    private float StartDazeTime;
    
    private int _speedy;
    private Animator _animator;

    private void Start()
    {
        _speedy = speed;
        _animator = GetComponent<Animator>();

        int dificulty = PlayerPrefs.GetInt("Difficulty");
        if (dificulty == 1) health = health * 2;
        if (dificulty == 2) health = health * 3;
        if (dificulty == 3) health = health * 5;
    }

    private void Update()
    {
        if (dazedTime <= 0)
        {
            speed = _speedy;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        if (health < 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(Hitted());
        }
    }

    IEnumerator Die()
    {
        _animator.SetTrigger("Dead");
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        //gameObject.GetComponent<Renderer>().material.color = Color.red;        
        yield return new WaitForSecondsRealtime(0.6f);
        _animator.SetTrigger("Destroyed");
        Destroy(gameObject);
    }

    IEnumerator Hitted()
    {
        _animator.SetBool("Hitted", true);
        yield return new WaitForSecondsRealtime(0.6f);
        _animator.SetBool("Hitted", false);
    }
}
