using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [Header("Explosion Parameters")]
    [SerializeField] private float explosionRadius;
    [SerializeField] private int explosionDamage;
    
    [Header("Camera Shake")] 
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(Explode());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Missile"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        //yield return new WaitForSeconds(0.25f);
        
        //shake screen
        CameraShake.instance.ShakeCamera(intensity,time);
        
        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Assuming the player has a method to take damage
                collider.GetComponent<Health>().TakeDamage(explosionDamage);
                
            }
            else if (collider.CompareTag("Enemy"))
            {
                // Assuming the enemy has a method to take damage
                collider.GetComponent<EnemyAI>().TakeDamage(explosionDamage);
            }
            else if (collider.CompareTag("Kamikaze"))
            {
                
                Destroy(collider.gameObject);
            }
        }

        Destroy(gameObject); // Destroy the barrel
        yield return null;
    }
}
