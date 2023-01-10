using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    private float fieldOfImpact;
    [SerializeField]
    private int force;
    [SerializeField]
    private LayerMask LayerToHit;
    [SerializeField]
    private int Health;
    private Player Player;
    private GettingHits PlayerGetHits;
    public GameObject ExplosionEffect;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Player =  player.GetComponent<Player>();
        PlayerGetHits = player.GetComponent<GettingHits>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            Health -= 1;
            if(Health <= 0) Explode();
        }
    }

    public void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(transform.position, new Vector2(fieldOfImpact, fieldOfImpact), 360, LayerToHit);
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                Player.Knockback(transform);
                PlayerGetHits.RegisterHit(gameObject);
            }
            else
            {
                Vector2 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * force * Time.deltaTime);
            }
        }
        GameObject ExplosionEffectInst = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffectInst, 1);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
