using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class KamikazeController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minSpeed = 2;
    [SerializeField] private float maxSpeed = 4;
    private float moveSpeed;

    [Header("Players")]
    [SerializeField] private GameObject player1; 
    [SerializeField] private GameObject player2; 
    private GameObject targetPlayer; 

    // Components
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
        ChooseClosestPlayer();
        if (targetPlayer != null)
        {
            _agent.SetDestination(targetPlayer.transform.position);
        }
    }

    private void ChooseClosestPlayer()
    {
        // Handle case where both players are null or inactive
        if ((player1 == null || !player1.activeSelf) && (player2 == null || !player2.activeSelf))
        {
            targetPlayer = null; 
            return;
        }

        // Handle case where only one player is active
        if (player1 != null && player1.activeSelf && (player2 == null || !player2.activeSelf))
        {
            targetPlayer = player1;
            return;
        }

        if (player2 != null && player2.activeSelf && (player1 == null || !player1.activeSelf))
        {
            targetPlayer = player2;
            return;
        }

        
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        
        targetPlayer = distanceToPlayer1 < distanceToPlayer2 ? player1 : player2;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            CameraShake.instance.ShakeCamera(10f, 0.25f);
            gameObject.SetActive(false);
        }
    }
}
