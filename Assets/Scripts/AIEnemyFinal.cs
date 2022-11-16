using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemyFinal : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject eyes;
    Transform player;
    public GameObject manSkin;
    public GameObject indicator;
    Animator anim;
    public bool wondering;
    public float wonderMaxStandStillTime;
    public float wonderMinStandStillTime;
    public float wonderMinWalkRange;
    public float wonderMaxWalkRange;
    public bool playerInVisionRegion;                           //is player inside vision trigger
    public float sightDistance;
    public LayerMask sightLayerMask;


    public enum states
    {
        search, chase, attack, chill, wonder
    }

    public states myState = new states();



    void Start()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = manSkin.GetComponent<Animator>();
        myState = states.wonder;
        indicator = Instantiate(indicator, new Vector3 (0,0,0), Quaternion.identity);
        StartCoroutine("CheckIfPlayerIsVisible");
        
    }

    
    void Update()
    {
        switch (myState)
        {
            case states.search:
                Search();
                break;

            case states.chase:
                Chase();
                break;

            case states.attack:
                Attack();
                break;

            case states.chill:
                Chill();
                break;

            case states.wonder:

                if (wondering == false)
                {
                    StartCoroutine("Wonder");
                }
                
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameController").GetComponent<GameController>().ResetLevel();
        }
        
    }

    void Search()
    {

    }

    void Chase()
    {
        agent.speed = 8;
        agent.acceleration = 35;
        agent.angularSpeed = 1000;
        anim.speed = 1f;

        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "BasicMotions@Sprint01 - Forwards")
        {
            print(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            anim.SetTrigger("Sprint");
        }
        
        agent.SetDestination(player.position);
    }

    void Attack()
    {

    }

    void Chill()
    {

    }

    IEnumerator Wonder()    //the agent wonders around and stands still sometimes
    {
        wondering = true;                                                                                   //bool for only running the coroutine once at a time
        agent.speed = 1.2f;
        agent.acceleration = 12;
        agent.angularSpeed = 200;
        anim.speed = 0.57f;

        while (myState == states.wonder)                                                        
        {
            Vector3 finalPosition = new Vector3(0,0,0);                                                     //this will become the position the agent will walk to
            NavMeshPath navMeshPath = new NavMeshPath();                                                    //navmeshpath for checking if a path is calculated (takes some time)
            bool navMeshPointFound = false;                                                                 //bool for checking for valid navmesh points

            while (navMeshPointFound == false)                                                              
            {
                float wonderRadius = Random.Range(wonderMinWalkRange, wonderMaxWalkRange);                  //random float
                Vector3 randomDirection = Random.insideUnitSphere * wonderRadius;                           //random position around agent
                randomDirection += transform.position + transform.forward * 0.25f;                          //random point + agent position + 1/4 of the search radius forward. this will make the agent prefer moving forward
                NavMeshHit hit;

                if (NavMesh.SamplePosition(randomDirection, out hit, 1, NavMesh.AllAreas) == true)          //finds the closest navmesh point to the randomDirection float within 1 unit of the point and returns false if no point exists
                {
                    finalPosition = hit.position;                                                           //if a point exists, it is saved here
                    navMeshPointFound = true;                                                               //bool set to true if point is found, taking us out of the while loop
                    //print("found point on navmesh");
                    
                }

                else
                {
                    //print("no point in range on navmesh");
                }              
            }

            agent.SetDestination(finalPosition);
            indicator.transform.position = finalPosition;
            //print("position set");

            while (agent.remainingDistance == 0)                                                            //it takes navmesh a little time to find a path to the target destination. during this time navmesh thinks the distance to the target is 0. Thats why I have this while loop.
            {
                //print("calculating path");
                yield return new WaitForSecondsRealtime(0.1f);
            }

            anim.SetTrigger("Walk");
            //print($"path found. distace: {agent.remainingDistance}");
            
            while (agent.remainingDistance > 0.01f)
            {


                
                

                //print($"moving. remaining distance: {agent.remainingDistance}");
                yield return new WaitForSecondsRealtime(0.2f);
            }

            anim.SetTrigger("Idle");
            //print("standing still");
            yield return new WaitForSecondsRealtime(Random.Range(wonderMinStandStillTime,wonderMaxStandStillTime));
        }

        wondering = false;
    }

    IEnumerator CheckIfPlayerIsVisible()
    {
        float checkDelay = 1;

        while (true)
        {
            if (playerInVisionRegion)
            {
                
                RaycastHit hit;
                Physics.Raycast(eyes.transform.position, player.position + new Vector3(0,1,0) - eyes.transform.position, out hit,  sightDistance, sightLayerMask, QueryTriggerInteraction.Ignore);
                Debug.DrawRay(eyes.transform.position, (player.position + new Vector3(0, 1, 0) - eyes.transform.position) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                print(hit.collider.name);

                if (hit.collider.transform.root.tag == "Player")
                {
                    Debug.Log("I see the player");

                    if (myState != states.chase)
                    {
                        myState = states.chase;
                    }
                }

                checkDelay = 0.2f;
            }

            else
            {
                checkDelay = 1;
            }

            yield return new WaitForSecondsRealtime(checkDelay);
        }
    }
}
