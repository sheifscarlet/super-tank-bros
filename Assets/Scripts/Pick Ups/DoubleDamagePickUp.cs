using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamagePickUp : MonoBehaviour
{
    [SerializeField] private float duration = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shooting shooting = other.GetComponent<Shooting>();
            if (shooting != null)
            {
                shooting.ActivateDoubleDamage(duration); 
            }

            
            AudioController.instance.PlaySound("PickUp");
            Destroy(gameObject);
        }
    }
}
