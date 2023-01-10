using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatistic : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;    
    private int health;

    [SerializeField]
    private int MaxShield = 100;    
    private int shield;

    public int numberOfGrenades = 3;
    public GameObject DeathScreen;
    private Player Player;
    public Healt_Bar_Shield_Bar_Controller healthShieldController;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        shield = MaxShield;
        healthShieldController.SetMaxHealth(health);
        healthShieldController.SetMaxShield(shield);

        DeathScreen = GameObject.FindWithTag("DeathScreen");
        DeathScreen.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        //Logick if has shields
        int healthDmg = 0;
        if (shield > 0)
        {
            if (shield - damage > 0) shield -= damage;
            else
            {
                shield = 0;
                healthDmg = damage - shield;
            }
        }
        else healthDmg = damage;

        //Update UI
        health -= healthDmg;
        healthShieldController.SetHealth(health);
        healthShieldController.SetShield(shield);

        if (health <= 0)
        {
            DeathScreen.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

    public void Heal(int heal)
    {        
        health += heal;
        if(health > MaxHealth) health = MaxHealth;
        healthShieldController.SetHealth(health);
    }
    public void PowerShield(int powerShield)
    {
        shield += powerShield;
        if (shield > MaxShield) shield = MaxShield;
        healthShieldController.SetShield(shield);
    }
}
