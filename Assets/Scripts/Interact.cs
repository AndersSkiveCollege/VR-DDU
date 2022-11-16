using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public AudioSource Audio;
    public float time;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Audio.Pause();
            time = Random.Range(20f, 40f);
        }
    }
    private void Update()
    {
        if (time >= -1)
        {
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            Audio.UnPause();
        }
    }
}
