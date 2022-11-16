using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallAnimal : MonoBehaviour
{
    public float walkRadius;

    void Start()
    {
        InvokeRepeating("Wonder", 1, Random.Range(0.5f,2f));
    }

    void Wonder() //method found online
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        GetComponent<NavMeshAgent>().destination = finalPosition;
    }
}
