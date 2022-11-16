using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLamp : MonoBehaviour
{
    public Color color = new Color();
    public float intensity;
    public bool on;

    //public static float electricalPower = 1;

    bool onLastUpdate;

    public Light myLight;
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public GameObject[] lampParts = new GameObject[4];

    
    void Update()
    {
        if (on == true)
        {
            myLight.color = color;
            myLight.intensity = intensity;
        }

        else
        {
            myLight.intensity = 0;
        }

        if (onLastUpdate != on)
        {
            if (on == true)
            {
                //electricalPower = electricalPower - 0.2f;
            }

            else
            {
                //electricalPower = electricalPower + 0.2f;
            }
        }

        onLastUpdate = on;
        ChangeLampMeterial();
    }

    //This method just changes the lamp to look like it's on or off. It doesn't actually affect the light.
    void ChangeLampMeterial()
    {
        if (on == true)
        {
            foreach (GameObject lampPart in lampParts)
            {
                lampPart.GetComponent<Renderer>().material = lightOnMaterial;
            }
        }

        else
        {
            foreach (GameObject lampPart in lampParts)
            {
                lampPart.GetComponent<Renderer>().material = lightOnMaterial;
            }
        }
    }
}
