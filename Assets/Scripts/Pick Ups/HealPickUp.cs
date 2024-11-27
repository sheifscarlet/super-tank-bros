using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioController.instance.PlaySound("HealPickUp");
            Health healthController = other.gameObject.GetComponent<Health>();
            healthController.ResetHealth();
            Destroy(gameObject);
        }
    }
}
