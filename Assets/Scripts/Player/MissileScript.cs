using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] int damageAmount;
    
    [Header("Camera Shake")] 
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;
    
    // Scoring
    private GameObject _shotBy;
    
    // Components
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        
        if (_rb != null)
        {
            _rb.velocity = transform.forward * speed;
            
        }
    }

    public void DestroyMissile()
    {
        gameObject.SetActive(false);
    }
    
    
    public void SetShotBy(GameObject shotBy)
    {
        _shotBy = shotBy;
    }

    private void Score(int points)
    {
        Score score = _shotBy.GetComponent<Score>();
        score.AddScore(points);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Kamikaze"))
        {
            CameraShake.instance.ShakeCamera(intensity,time);
            Destroy(other.gameObject);
            DestroyMissile();
            Score(100);
        } 
        else if (other.CompareTag("Enemy"))
        {
            CameraShake.instance.ShakeCamera(intensity,time);
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(damageAmount);
            DestroyMissile();
            if (enemy.GetCurrentHealth() <= 0)
            {
                Score(100);
            }
            else
            {
                Score(10);
            }
        }
        else if(other.CompareTag("Environment"))
        {
            CameraShake.instance.ShakeCamera(1.5f,time);
            DestroyMissile();
        }
    }
}
