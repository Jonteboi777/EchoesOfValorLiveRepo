using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool detected;
    GameObject target;
    public Transform Enemy;

    public GameObject bullet;
    public Transform ShootPoint;
    public AudioSource adSourceFire;
    public AudioSource adSourceArm;

    public float shootSpeed = 10f;
    public float timeToShoot = 1.3f;
    float originalTime;

    [Header("General")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public DetectScript detector;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        originalTime = timeToShoot;
    }

    public void Awake()
    {
        player = GameObject.Find("PlayerWolffe").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        detector.isPlayerdetected = detected;

        if (detected)
        {
            Enemy.LookAt(target.transform);
            timeToShoot -= Time.deltaTime;
            if (timeToShoot < 0)
            {
                ShootPlayer();
                timeToShoot = originalTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootPlayer();
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
    }
    private void ShootPlayer()
    {
        GameObject currentBullet = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
        Rigidbody rig = currentBullet.GetComponent<Rigidbody>();

        rig.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);

        adSourceFire.Play();
    }
}
