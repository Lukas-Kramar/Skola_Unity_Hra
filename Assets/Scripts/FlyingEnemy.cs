using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    [SerializeField]
    public int speed = 300;

    [SerializeField]
    public int chaseDistance = 50;

    private float dazedTime;
    [SerializeField]
    private float StartDazeTime;

    private int _speedy;
    private Animator _animator;

    [SerializeField]
    private float attackAnimationRange;

    private bool isAttacking;
    private Rigidbody2D _rigidbody2;
    
    private void Start()
    {
        _speedy = speed;

        _animator = GetComponent<Animator>();
        _rigidbody2 = GetComponent<Rigidbody2D>();

        int dificulty = PlayerPrefs.GetInt("Difficulty");
        if (dificulty == 1) health = health * 2;
        if (dificulty == 2) health = health * 3;
        if (dificulty == 3) health = health * 5;
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Collider2D[] objects = Physics2D.OverlapBoxAll(_rigidbody2.position, new Vector2(attackAnimationRange, attackAnimationRange), 360);
            foreach (Collider2D obj in objects)
            {
                if (obj.CompareTag("Player"))
                {
                    StartCoroutine(Attack());
                }
                //else
                //{
                //    _animator.SetBool("Attack", false);
                //}
            }
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
        _rigidbody2.constraints = RigidbodyConstraints2D.FreezePositionX;
        _rigidbody2.gravityScale = 500;
        yield return new WaitForSecondsRealtime(0.6f);
        _animator.SetTrigger("Destroyed");
        Destroy(gameObject);
    }

    IEnumerator Hitted()
    {
        _animator.SetBool("Hitted", true);
        yield return new WaitForSecondsRealtime(0.4f);
        _animator.SetBool("Hitted", false);
    }
    IEnumerator Attack()
    {
        //UnityEngine.Debug.Log("setAttackAnimation");
        isAttacking = true;
        _animator.SetBool("Attack", true);
        yield return new WaitForSecondsRealtime(0.6f);
        _animator.SetBool("Attack", false);
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAnimationRange);
    }
}

