using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("Patrolling")]
    [SerializeField] private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    [Header("Attacking")]
    [SerializeField] private GameObject turretObj;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float missileSpeed;
    private bool alreadyAttacked;
    [SerializeField] private GameObject projectile;

    [Header("States")]
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    [Header("Players")]
    [SerializeField] private Transform player1; 
    [SerializeField] private Transform player2; 
    private Transform targetPlayer; 

    [Header("Components")]
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

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
        ChooseClosestPlayer();

        if (targetPlayer != null)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Vector3.Distance(transform.position, targetPlayer.position) <= attackRange;

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        else
        {
            Patroling();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (targetPlayer != null)
        {
            agent.SetDestination(targetPlayer.position);
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        RotateTurretTowardsPlayer();

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, turretObj.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(turretObj.transform.forward * missileSpeed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void RotateTurretTowardsPlayer()
    {
        if (targetPlayer == null) return;

        Vector3 direction = targetPlayer.position - turretObj.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turretObj.transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        turretObj.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ChooseClosestPlayer()
    {
        // Handle case where both players are null or inactive
        if ((player1 == null || !player1.gameObject.activeSelf) && (player2 == null || !player2.gameObject.activeSelf))
        {
            targetPlayer = null; 
            return;
        }

        // Handle case where only one player is active
        if (player1 != null && player1.gameObject.activeSelf && (player2 == null || !player2.gameObject.activeSelf))
        {
            targetPlayer = player1;
            return;
        }
        if (player2 != null && player2.gameObject.activeSelf && (player1 == null || !player1.gameObject.activeSelf))
        {
            targetPlayer = player2;
            return;
        }

        // Handle case where both players are active
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.position);

        
        targetPlayer = distanceToPlayer1 < distanceToPlayer2 ? player1 : player2;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
        if (currentHealth <= 0)
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
