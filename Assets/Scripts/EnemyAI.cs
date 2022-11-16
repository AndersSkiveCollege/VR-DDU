using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float countDown = 5;
    public NavMeshAgent agent;

    Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private float randomZ;
    private float randomX;
    public float StillTimer;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkRadius;
    Vector3 randomDirection;
    public float minStillTime;
    public float maxStillTime;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //switch

    public enum states
    {
        patrol, chase, attack
    }
    public states myState = new states();

    //damage
    //public GameObject playerDamage;
    public GameObject manSkin;
    Animator anim;

    private void Awake()
    {
        //Getiing player transform and agent nav-mesh agent
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = manSkin.GetComponent<Animator>();
    }


    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //state-machine controller
        if (!playerInSightRange && !playerInAttackRange)
        {
            myState = states.patrol;
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            myState = states.chase;
        }
        if (playerInSightRange && playerInAttackRange)
        {
            myState = states.attack;
        }

        switch (myState)
        {
            case states.patrol:
                patroling();
                break;

            case states.chase:
                chasePlayer();
                break;

            case states.attack:
                attackPlayer();
                break;
        }
        if (StillTimer >= 0)
        {
            StillTimer = StillTimer - Time.deltaTime;
            anim.SetInteger("Movement", 0);
        }
    }


    private void patroling()
    {
        agent.speed = 5;
        anim.SetInteger("Movement", 1);
        //walk to walkpoint
        if (!walkPointSet)
        {
            StillTimer = Random.Range(minStillTime, maxStillTime);
            SearchWalkPoint();
        }

        if (walkPointSet & StillTimer <= 0)
        {
            agent.SetDestination(walkPoint);

        }

        float distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);

        //walkpoin reached
        if (distanceToWalkPoint < 1)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        walkPointSet = true;
        newWalkPoint();
    }
    //placing new walkpoint
    private void newWalkPoint()
    {
        randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        walkPoint = hit.position;
    }
    private void chasePlayer()
    {
        agent.speed = 9;
        //placing destination to the player
        agent.SetDestination(player.position);
        anim.SetInteger("Movement", 1);
    }
    private void attackPlayer()
    {
        agent.speed = 15;
        //stopping up and attacking
        agent.SetDestination(transform.position);
        anim.SetInteger("Movement", 0);
        if (!alreadyAttacked)
        {
            //attack cooldown
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            anim.SetInteger("Movement", 2);
            alreadyAttacked = true;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
