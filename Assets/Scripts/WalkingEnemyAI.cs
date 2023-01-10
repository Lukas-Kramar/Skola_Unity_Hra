using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.TerrainUtils;

public class WalkingEnemyAI : MonoBehaviour
{
    private WalkingEnemy _basicEnemy;
    private Transform target;
    public float AttackRangeAnimation = 2;

    public Transform enemyGFX;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _basicEnemy = GetComponent<WalkingEnemy>();       
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        var xVelocity = target.position.x - _rigidbody.position.x;
        //Debug.Log(xVelocity);
        if((xVelocity <= AttackRangeAnimation && xVelocity > 0) || (xVelocity <= 0 && xVelocity > -AttackRangeAnimation))
        {
            StartCoroutine(Attack());
        }
        if (xVelocity > -(_basicEnemy.chaseDistance) && xVelocity < _basicEnemy.chaseDistance)
        {
            Vector2 velocity = new Vector2(0f, 0f);
            if (xVelocity < 0)
            {
                velocity.x = -_basicEnemy.speed;
            }
            else
            {
                velocity.x = _basicEnemy.speed;
            }
            _rigidbody.velocity = velocity * Time.deltaTime;            

            //Sprite turning
            if (_rigidbody.velocity.x <= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (_rigidbody.velocity.x >= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            _animator.SetFloat("Speed", 10);
        }      
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    IEnumerator Attack()
    {
        _animator.SetBool("Attack", true);
        yield return new WaitForSecondsRealtime(0.6f);
        _animator.SetBool("Attack", false);
    }
}
