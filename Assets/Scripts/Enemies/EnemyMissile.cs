using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float selfDestructTime = 2.5f;


    private void OnEnable()
    {
        Invoke(nameof(DestroyEnemyMissile), selfDestructTime);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(DestroyEnemyMissile));
    }

    public void DestroyEnemyMissile()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Environment"))
        {
            DestroyEnemyMissile();
        }
        else if(other.CompareTag("Player"))
        {
            DestroyEnemyMissile();
        }
    }
}
