using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentVisionCone : MonoBehaviour
{
    public AIEnemyFinal aIEnemyFinal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            aIEnemyFinal.playerInVisionRegion = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            aIEnemyFinal.playerInVisionRegion = false;
        }
    }
}
