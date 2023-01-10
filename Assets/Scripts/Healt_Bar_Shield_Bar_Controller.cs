using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healt_Bar_Shield_Bar_Controller : MonoBehaviour
{
    public Slider healtSlider;
    public Slider shieldSlider;

    public void SetMaxHealth(int health)
    {
        healtSlider.maxValue = health;
        healtSlider.value = health;
    }

    public void SetMaxShield(int shield)
    {
        shieldSlider.maxValue = shield;
        shieldSlider.value = shield;
    }

    public void SetHealth(int health)
    {
        healtSlider.value = health;
    }
    
    public void SetShield(int shield)
    {
        shieldSlider.value = shield;
    }

}
