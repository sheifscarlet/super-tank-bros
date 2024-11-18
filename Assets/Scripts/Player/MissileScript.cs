using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] int damageAmount;
    
    [Header("Camera Shake")] 
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;
    // Components
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // При активации ракеты задаем ей начальную скорость
        if (_rb != null)
        {
            _rb.velocity = transform.forward * speed;
            
        }
    }

    public void DestroyMissile()
    {
        
        gameObject.SetActive(false);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Kamikaze"))
        {
            CameraShake.instance.ShakeCamera(intensity,time);
            Destroy(other.gameObject);
            DestroyMissile();
            VFXController.instance.PlaySFX(1,transform.position);
        } 
        else if (other.CompareTag("Enemy"))
        {
            CameraShake.instance.ShakeCamera(intensity,time);
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(damageAmount);
            DestroyMissile();
            VFXController.instance.PlaySFX(0,transform.position);
        }
        else if(other.CompareTag("Environment"))
        {
            CameraShake.instance.ShakeCamera(1.5f,time);
            VFXController.instance.PlaySFX(0,transform.position);
            DestroyMissile();
        }
    }
}