using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour, IDamagable
{
    [Header("Health")] 
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    
    [Header("Patroling")] 
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    [Header("Attacking")] 
    [SerializeField] private GameObject turretObj;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] private float missileSpeed;
    bool alreadyAttacked;
    [SerializeField] GameObject projectile;
    
    [Header("States")] 
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;
    
    [Header("Components")] 
    [SerializeField] Transform player;
    private NavMeshAgent agent;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
    
        if (walkPointSet)
            agent.SetDestination(walkPoint);
    
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
    
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
    
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
    
        RotateTurretTowardsPlayer();
        
        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(projectile, turretObj.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(turretObj.transform.forward * missileSpeed, ForceMode.Impulse);
            //End of attack code
    
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void RotateTurretTowardsPlayer()
    {
        Vector3 direction = player.position - turretObj.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turretObj.transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        turretObj.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void Dead()
    {
        VFXController.instance.PlaySFX(1,transform.position);
        gameObject.SetActive(false);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
