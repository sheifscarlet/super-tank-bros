using System.Collections;
using UnityEngine;

public class MissleSpawner : MonoBehaviour
{
    [Header("Shooting Point")]
    [SerializeField] private Transform shootPoint;

    [Header("Object Pool")]
    [SerializeField] private ObjectPool missilePoolObj;

    public GameObject Spawn(int damage)
    {
        GameObject missile = missilePoolObj.GetPooledObject();
        if (missile != null)
        {
            
            missile.transform.position = shootPoint.position;
            missile.transform.rotation = shootPoint.rotation;
            missile.SetActive(true);
            
            MissileScript missileScript = missile.GetComponent<MissileScript>();
            if (missileScript != null)
            {
                missileScript.SetDamage(damage);
            }
            
            // Scoring
            missile.GetComponent<MissileScript>().SetShotBy(gameObject);
        }
        return missile;
    }
}