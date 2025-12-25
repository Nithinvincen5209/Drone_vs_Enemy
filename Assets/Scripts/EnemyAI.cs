using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] waypoints;
    public float patrolSpeed;

    [Header("Firing Settings")]
    public GameObject bulletPrefab;
    public float detectionRange;
    public Transform firePoint;
    private float fireRate = 1f;

    private NavMeshAgent agent;
    private Transform player;
    private int currentwaypointIndex = 0;
    private int nextfireRate;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        // Finding Player Taged Gameobject.
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Checking distacneToPlayer lesserthan or Equal detectionRange.
        if (distanceToPlayer <= detectionRange)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }
    void AttackPlayer()
    {
        // Is stopping Navmesh Agent.
        agent.isStopped = true;

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        if (Time.time >= nextfireRate)
        {
            nextfireRate = (int)(Time.time + fireRate);
            Shoot();
        }
    }
    void Patrol()
    {
        // Setting up Enemy Patrol.
        agent.isStopped = false;
        agent.SetDestination(waypoints[currentwaypointIndex].position);
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            currentwaypointIndex = (currentwaypointIndex + 1) % waypoints.Length;
        }
    }
    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,detectionRange);
    }
}
