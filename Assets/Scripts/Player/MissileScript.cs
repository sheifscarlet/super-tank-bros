using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private int damageAmount; 
    [SerializeField] private float selfDestructTime = 2.5f;
    
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
        Invoke(nameof(DestroyMissile), selfDestructTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DestroyMissile));
    }

    public void DestroyMissile()
    {
        gameObject.SetActive(false);
        _shotBy = null;
    }
    
    public void SetShotBy(GameObject shotBy)
    {
        _shotBy = shotBy;
    }

    public GameObject GetShotBy()
    {
        return _shotBy;
    }

    private void Score(int points)
    {
        Score score = _shotBy.GetComponent<Score>();
        score.AddScore(points);
    }
    
    public void SetDamage(int damage)
    {
        damageAmount = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kamikaze"))
        {
            ParticleSystemController.Instance.PlayVFX("Hit",transform.position,Quaternion.identity);
            CameraShake.instance.ShakeCamera(intensity, time);
            Destroy(other.gameObject);
            Score(100);
            DestroyMissile();
        } 
        else if (other.CompareTag("Enemy"))
        {
            
            CameraShake.instance.ShakeCamera(intensity, time);
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(damageAmount);
            if (enemy.GetCurrentHealth() <= 0)
            {
                Score(100);
            }
            else
            {
                Score(10);
            }
            DestroyMissile();
        }
        else if (other.CompareTag("Environment"))
        {
            ParticleSystemController.Instance.PlayVFX("Hit",transform.position,Quaternion.identity);
            CameraShake.instance.ShakeCamera(1.5f, time);
            DestroyMissile();
        }
    }
}