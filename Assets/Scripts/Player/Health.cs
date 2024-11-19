using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    
    [Header("Camera Shake")] 
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damageAmount)
    {
        CameraShake.instance.ShakeCamera(intensity,time);
        
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    
    public void Heal(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        }
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMissile"))
        {
            TakeDamage(1);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Kamikaze"))
        {
            TakeDamage(2);
        }
    }
}
