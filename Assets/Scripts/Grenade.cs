using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    [SerializeField]
    private float fieldOfImpact;
    [SerializeField]
    private int explosionForce;
    [SerializeField]
    private int grenadeDMG;
    float countdown;
    [SerializeField]
    private GameObject explosionEffect;
    private Player Player;
    private GettingHits PlayerGetHits;
    private bool hasExploded = false;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Player = player.GetComponent<Player>();
        PlayerGetHits = player.GetComponent<GettingHits>();
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider2D[] objects = Physics2D.OverlapBoxAll(transform.position, new Vector2(fieldOfImpact, fieldOfImpact), 360);
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                Player.Knockback(transform);
                PlayerGetHits.RegisterHit(gameObject);
            }
            if (obj.CompareTag("Barrel"))
            {
                ExplosiveBarrel barel = obj.GetComponent<ExplosiveBarrel>();
                barel.Explode();
            }
            if (obj.CompareTag("BasicEnemy"))
            {
                WalkingEnemy basicEnemy = obj.GetComponent<WalkingEnemy>();
                basicEnemy.TakeDamage(grenadeDMG);
            }

            Vector2 direction = obj.transform.position - transform.position;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce * Time.deltaTime);

            }
        }
        GameObject ExplosionEffectInst = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffectInst, 1);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
