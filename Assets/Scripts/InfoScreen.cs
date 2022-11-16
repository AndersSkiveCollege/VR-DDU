using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScreen : MonoBehaviour
{
    // Variabler 
    public Renderer rend;
    public Material[] allebilederne;
    public int nuvearendebillede;

    // Start is called before the first frame update
    void Start()
    {
        // Laver en Coroutine som skifter farve
       // StartCoroutine(ManyColors());     DEAKTIVERET PGA FEJL
    }

    IEnumerator ManyColors()
    {
      
        while (true)
        {
            
            if (nuvearendebillede < allebilederne.Length - 1)
            {
                nuvearendebillede = nuvearendebillede + 1;

                rend.material = allebilederne[nuvearendebillede];
            }

            else
            {
                nuvearendebillede = 0;
                rend.material = allebilederne[nuvearendebillede];
            }

            yield return new WaitForSeconds(3f);
        }
    }

}
