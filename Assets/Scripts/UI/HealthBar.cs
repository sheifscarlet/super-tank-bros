using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI Components")]
    public Slider healthSlider;
    

    [Header("Health System")]
    [SerializeField] Health playerHealth;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth == null)
        {
            return;
        }
        
        healthSlider.maxValue = playerHealth.MaxHealth;
        healthSlider.value = playerHealth.CurrentHealth;
        
        
        playerHealth.OnHealthChanged += UpdateHealthBar;
    }
    
    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    
    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }
}