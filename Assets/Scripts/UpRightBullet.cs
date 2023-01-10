using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpRightBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Basic_Weapon basic_Weapon;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        basic_Weapon = player.GetComponent<Basic_Weapon>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(1,1,0) * basic_Weapon.bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        WalkingEnemy enemy = collision.GetComponent<WalkingEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(basic_Weapon.bulletDMG);
        }
        FlyingEnemy flyingEnemy = collision.GetComponent<FlyingEnemy>();
        if (flyingEnemy != null)
        {
            flyingEnemy.TakeDamage(basic_Weapon.bulletDMG);
        }
        PykeEnemy pykeEnemy = collision.GetComponent<PykeEnemy>();
        if (pykeEnemy != null)
        {
            pykeEnemy.TakeDamage(basic_Weapon.bulletDMG);
        }
        Destroy(gameObject);
    }
}
