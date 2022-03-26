using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject projectile;


    public Vector3 dekh;

    public Vector3 plaPos;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        //agent.speed = 120;
        //agent.angularSpeed = 120;
        //agent.acceleration = 30;

    }

    private void Update()
    {
        StartCoroutine("Checku");

        playerInAttackRange = Vector3.Distance(transform.position, player.position) <= attackRange;

        if (playerInAttackRange) AttackPlayer();
        else ChasePlayer();

        //Debug.Log(walkpoint);
        //Debug.Log(walkPointSet);
    }

    //private void Patroling()
    //{
    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet) agent.SetDestination(walkpoint);       

    //    Vector3 distanceToWalkPoint = transform.position - walkpoint;

    //    if (distanceToWalkPoint.magnitude < 0.1f) walkPointSet = false;
    //}

    //private void SearchWalkPoint()
    //{
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

       
    //    NavMeshPath navMeshPath = new NavMeshPath();
    //    if (agent.CalculatePath(walkpoint, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) Debug.Log("hossena");

    //    Debug.Log(navMeshPath);

    //    //if(Physics.Raycast(walkpoint, -transform.up, transform.localScale.y * 2, whatIsGround)) walkPointSet = true;

    //    //walkPointSet = true;
    //    //Debug.DrawLine(walkpoint, walkpoint - transform.up * transform.localScale.y, Color.blue);
    //    //Debug.DrawRay(walkpoint, -transform.up * transform.localScale.y * 2, Color.red, 100);
    //}

    private void ChasePlayer()
    {
        Vector3 go = player.position;
        agent.SetDestination(go);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(plaPos);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position + transform.forward * 5 + transform.up, transform.rotation).GetComponent<Rigidbody>();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    IEnumerator Checku()
    {
        yield return new WaitForSeconds(5);
        plaPos = player.position;
        StartCoroutine("Checku");
    }
}


