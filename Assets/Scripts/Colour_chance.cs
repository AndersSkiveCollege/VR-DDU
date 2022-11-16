using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_chance : MonoBehaviour
{
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ManyColors());
    }

    IEnumerator ManyColors()
    {
        for(int i = 0; i < 50; i++)
        {
            rend.material.color += new Color(0.03f, 0.03f, 0.03f);
            yield return new WaitForSeconds(0.05f); 
        }
    }

}
