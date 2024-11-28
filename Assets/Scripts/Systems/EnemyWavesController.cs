using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
    public static EnemyWavesController instance;
    [SerializeField] private List<GameObject> waves; 
    private int currentWaveIndex = 0;
    public bool allWavesCleared = false; 
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (waves.Count > 0)
        {
            waves[currentWaveIndex].SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (waves.Count > currentWaveIndex && AreAllEnemiesDead(waves[currentWaveIndex]))
        {
            // Move to the next wave
            currentWaveIndex++;

            if (currentWaveIndex < waves.Count)
            {
                // Activate the next wave
                waves[currentWaveIndex].SetActive(true);
            }
        }
        
        // Check if all waves have been completed and all enemies are cleared
        if (currentWaveIndex >= waves.Count)
        {
            allWavesCleared = true;
        }
    }
    
    
    private bool AreAllEnemiesDead(GameObject wave)
    {
        // Find all active enemy objects in the wave and check if they are all dead
        foreach (Transform child in wave.transform)
        {
            if (child.gameObject.activeSelf)
            {
                return false;
            }
        }
        
        return true;
    }
    
}
