using System.Collections;
using UnityEngine;

public class MissleSpawner : MonoBehaviour, ISpawner
{
    [Header("Shooting Point")]
    [SerializeField] private Transform shootPoint; // Shooting point for this player

    [Header("Object Pool")]
    [SerializeField] private ObjectPool missilePoolObj;

    public void Spawn()
    {
        GameObject missile = missilePoolObj.GetPooledObject();
        if (missile != null)
        {
            // Set missile position and rotation
            missile.transform.position = shootPoint.position;
            missile.transform.rotation = shootPoint.rotation;
            missile.SetActive(true);
            
            // Scoring
            missile.GetComponent<MissileScript>().SetShotBy(gameObject);
        }
    }
}