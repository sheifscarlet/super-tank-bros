using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    public static PickupSpawner instance;
    [SerializeField] private GameObject[] pickupPrefabs; 
    [SerializeField] private int maxPickups = 5; 
    [SerializeField] private NavMeshSurface navMeshSurface;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnMaxPickups();
    }

    public void SpawnMaxPickups()
    {
        for (int i = 0; i < maxPickups; i++)
        {
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        if (randomPoint != Vector3.zero)
        {
            GameObject pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];
            Instantiate(pickupPrefab, randomPoint, Quaternion.identity);
        }
    }

    
    private Vector3 GetRandomPointOnNavMesh()
    {
        // Calculate the bounds of the NavMeshSurface
        Bounds navMeshBounds = navMeshSurface.navMeshData.sourceBounds;

        for (int attempt = 0; attempt < 10; attempt++) // Try multiple times to find a valid point
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(navMeshBounds.min.x, navMeshBounds.max.x),
                0f,
                Random.Range(navMeshBounds.min.z, navMeshBounds.max.z)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                return hit.position; // Valid point found
            }
        }

        return Vector3.zero; // No valid point found after multiple attempts
    }
}
