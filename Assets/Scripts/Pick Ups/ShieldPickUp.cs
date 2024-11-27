using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Health playerHealthController = other.gameObject.GetComponent<Health>();
            playerHealthController.ActivateShield(duration);
            AudioController.instance.PlaySound("PickUp");
            Destroy(gameObject);
        }
    }
}
