using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHits : MonoBehaviour
{
    private PlayerStatistic playerStatistic;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int spikeDMG;
    [SerializeField]
    private int basicEnemyDMG;
    [SerializeField]
    private int BarrelExplosionDMG;
    [SerializeField]
    private int GrenadeExplosionDMG;
    [SerializeField]
    private int invicibilityTime;


    private bool invicible = false;
    private void Start()
    {
        playerStatistic = GetComponent<PlayerStatistic>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void RegisterHit(int DMG)
    {
        playerStatistic.TakeDamage(DMG);
        StartCoroutine(BecomeInvincible());
    }
    public void RegisterHit(GameObject From)
    {
        if (From.CompareTag("Barrel")) RegisterHit(BarrelExplosionDMG);
        else if (From.CompareTag("Grenade")) RegisterHit(GrenadeExplosionDMG);
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invicible == true) return;
        if (collision.gameObject.CompareTag("Spikes"))
        {
            RegisterHit(spikeDMG);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (invicible == true) return;
        if (collision.gameObject.CompareTag("Spikes"))
        {
            RegisterHit(spikeDMG);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invicible == true) return;
        if (collision.gameObject.CompareTag("BasicEnemy"))
        {
            RegisterHit(basicEnemyDMG);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (invicible == true) return;
        if (collision.gameObject.CompareTag("BasicEnemy"))
        {
            RegisterHit(basicEnemyDMG);
        }
    }

    IEnumerator BecomeInvincible()
    {        
        invicible = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(invicibilityTime);
        spriteRenderer.color = Color.white;
        invicible = false;
    }
}
