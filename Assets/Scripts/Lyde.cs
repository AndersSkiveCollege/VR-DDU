using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lyde : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(Gaalyd());
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<EnemyAI>();
        

    }

    IEnumerator Gaalyd()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(3);
        }


    }


}
