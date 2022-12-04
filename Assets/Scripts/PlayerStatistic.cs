using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatistic : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;
    [SerializeField]
    private int health;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x - 5, gameObject.transform.position.y, gameObject.transform.position.z);
        if(health <= 0)
        {
            Debug.Log("Chcipnul si");
        }
    }

    public void TakeMeds(int heal)
    {        
        health += heal;
        if(health > MaxHealth) health = MaxHealth;
        healthBar.SetHealth(health);
    }
}
