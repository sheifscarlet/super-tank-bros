using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public void DestroyMissile()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Environment"))
        {
            DestroyMissile();
        }
        else if(other.CompareTag("Player"))
        {
            DestroyMissile();
        }
    }
}
