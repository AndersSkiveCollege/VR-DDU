using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchLight : MonoBehaviour
{

    public List<GameObject> peopleInYard;
    public bool following;
    public GameObject searchLightObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (peopleInYard.Count > 0 && following == false)
        {

            StartCoroutine(FollowSomePerson());


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            peopleInYard.Add(other.gameObject); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            peopleInYard.Remove(other.gameObject);
        }
    }

    IEnumerator FollowSomePerson()
    {
        following = true;
        int randomPerson = Random.Range(0, peopleInYard.Count);
        while (following == true)
        {
            if (peopleInYard.Count < 1)
            {
                following = false;
                break;
            }

            if (peopleInYard[randomPerson] == null)
            {
                following = false;
                continue;
            }

            searchLightObj.transform.LookAt(peopleInYard[randomPerson].transform.position);
            yield return new WaitForSeconds(0.02f);
        }
        
    }

    
}
