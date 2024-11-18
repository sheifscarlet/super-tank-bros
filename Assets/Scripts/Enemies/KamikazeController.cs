using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class KamikazeController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minSpeed = 2;
    [SerializeField] private float maxSpeed = 4;
    private float moveSpeed;
    
    //Components
    [SerializeField] private GameObject player;
    private NavMeshAgent _agent;
    private Rigidbody _rb;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        _agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveToPlayer();
        _agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            CameraShake.instance.ShakeCamera(10f,0.25f);
            gameObject.SetActive(false);
            VFXController.instance.PlaySFX(1,transform.position);
        }
    }
}
