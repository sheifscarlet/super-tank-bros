using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;
    
    [SerializeField] bool isDead = false;
    public bool IsDead => isDead;

    [SerializeField] bool canTakeDamage = true;
    private Coroutine shieldCoroutine;
    [SerializeField] private GameObject shieldVFX;
    
    [Header("Camera Shake")] 
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;
    
    public event Action<int, int> OnHealthChanged;
    
    // Start is called before the first frame update
    void Start()
    {
        shieldVFX.SetActive(false);
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage)
        {
            CameraShake.instance.ShakeCamera(intensity,time);
            
            if(currentHealth > 0)
            {
                ParticleSystemController.Instance.PlayVFX("Hit",transform.position,Quaternion.identity);
                AudioController.instance.PlaySound("Damage");
                currentHealth -= damageAmount;
                OnHealthChanged?.Invoke(currentHealth, maxHealth);
            }
            if(currentHealth <= 0)
            {
                Dead();
            }
        }
         
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    public void Dead()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ParticleSystemController.Instance.PlayVFX("Dead",transform.position,Quaternion.identity);
        AudioController.instance.PlaySound("Death");
        gameObject.SetActive(false);
        isDead = true;
    }

    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine); // Stop the current coroutine to reset the timer
        }

        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        canTakeDamage = false; 
        shieldVFX.SetActive(true);

        yield return new WaitForSeconds(duration); 

        canTakeDamage = true; 
        shieldVFX.SetActive(false);
        shieldCoroutine = null; // Reset the coroutine reference
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
