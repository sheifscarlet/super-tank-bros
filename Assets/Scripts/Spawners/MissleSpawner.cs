using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MissleSpawner : MonoBehaviour, ISpawner
{
    
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ObjectPool misslePoolObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        VFXController.instance.PlaySFX(2,shootPoint.position);
        GameObject missle = misslePoolObj.GetPooledObject();
        if (missle != null)
        {
            missle.transform.position = shootPoint.position;
            missle.transform.rotation = shootPoint.rotation;
            missle.SetActive(true);
            
        }
    }
}
