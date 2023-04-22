using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float currentHealth;
    public int maxHealth = 100;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    // Start is called before the first frame update
    public void SetHealth(float health){
        slider.value = health;
    }

    public float GetHealth(){
        return slider.value;
    }
}
