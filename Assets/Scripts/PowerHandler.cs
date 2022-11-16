using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerHandler : MonoBehaviour
{
    public float power;
    public float powerConsumption;
    public bool powerOff;
    public PlayerClick playerClick; // for turning off all the screens
    public List<Light> allPoweredLights = new List<Light>(); //all lights on the powergrid
    public List<float> allPoweredLightIntensities = new List<float>(); //all lght intensities
    private Light[] everyLightsourceInTheScene; //literally all lights
    public Material lightEmitterMat;
    Material lightEmitterMatCopy;
    Color emissionColor = new Color(0.8f, 0.8f, 0.1f);
    public int hej;
    public TextMeshProUGUI screenText;
    public bool powerFluctuating;
    public float fluctuationChance;
    public float ceilingLampIntensity; //used for single light fluctuations


    void Start()
    {
        GetAllLightsInScene();
        lightEmitterMat.SetColor("_EmissionColor", emissionColor);
        lightEmitterMatCopy = new Material(lightEmitterMat);
        InvokeRepeating("ChanceToStartRandomPowerFluctuation", 0, 1);
        StartCoroutine("SingleLightFluctuation");
    }
    
    void Update()
    {
        power -= Time.deltaTime;
        UpdatePowerMonitor();

        if (power <= 0 && powerOff == false)
        {
            PowerOff(); 
        }

        else if (power > 0)
        {
            PowerOn();
        }
  
    }

    void ChanceToStartRandomPowerFluctuation()
    {
        //print(fluctuationChance / power);
        if (powerOff || powerFluctuating == true)
        {
            return;
        }
        
        if (power == 0)
        {
            return;
        }

        else if (fluctuationChance/power > Random.Range(0f,1f))
        {
            if (Random.Range(0,2) > 0)
            {
                StartCoroutine("SlowPowerFluctuation");
                
            }

            if (Random.Range(0, 2) > 0)
            {
                StartCoroutine("FastPowerFluctuation");

            }

        }
    }

    void UpdatePowerMonitor()
    {

        if (power > 60) // inf to 60
        {
            screenText.text = $"Power:  {power.ToString()}";
        }

        else if(power > 10) // 60 to 10
        {
            screenText.text = $"Power:  {power.ToString()} \nWarning! \nLow power.";
        }

        else if (power > 3) // 10 to 3
        {
            screenText.text = $"Power:  {power.ToString()} \nWarning! \nPower level: \nCritical!";
        }

        else if (power > 0) // 3 to 0
        {
            screenText.text = $"Power:  {power.ToString()} \nSYSTEM FAILURE";
        }

        else if (power < 0) // below 0
        {
            screenText.text = "";
        }
        
    }

    void GetAllLightsInScene() 
    {
        everyLightsourceInTheScene = FindObjectsOfType(typeof(Light)) as Light[];

        foreach (Light light in everyLightsourceInTheScene)
        {
            if (light.name != "FlashLight")
            {
                allPoweredLights.Add(light);
                allPoweredLightIntensities.Add(light.intensity);
                if (light.name == "Point Light Ceiling")
                {
                    light.intensity = ceilingLampIntensity;
                }
            }
        }
    }

    IEnumerator SingleLightFluctuation() //makes x % of ceiling lights flicker through out the game
    {
        Color thisEmissionColor = new Color(1,1,1);
        
        float lightIntensity = 1;
        int counter = 0;
        

        List<Light> affectedLights = new List<Light>();

        foreach (Light light in allPoweredLights)
        {
            if (light.name == "Point Light Ceiling")
            {
                if (Random.Range(0,8) == 1) // % of affected lights
                {
                    affectedLights.Add(light);

                    Transform[] lightParts = light.transform.parent.GetComponentsInChildren<Transform>();

                    foreach (Transform trans in lightParts)
                    {
                        if (trans.gameObject.name == "pipe")
                        {
                            trans.gameObject.GetComponent<MeshRenderer>().material = lightEmitterMatCopy;
                        }
                        
                        
                        
                    }

                    //light.transform.parent. GetComponent<Material>(). = lightEmitterMatCopy;
                }
            }
        }

        if (affectedLights.Count < 1)
        {
            yield break;
        }


        while (powerOff == false)
        {
            float randomRiseRate = Random.Range(1.0f, 1.0f);
            float randomFallRate = Random.Range(1.0f, 1.0f);
            float randomMax = Random.Range(0.8f, 1f);
            float randomMin = Random.Range(0.0f, 0.2f);


            while (lightIntensity > randomMin)
            {
                if (powerOff == true)
                {
                    PowerOff();
                    yield break;
                }

                lightIntensity -= randomFallRate;

                foreach (Light light in affectedLights)
                {
                    yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
                    light.intensity = lightIntensity * ceilingLampIntensity;
                    
                    thisEmissionColor = new Color(lightIntensity, lightIntensity, lightIntensity);
                    lightEmitterMatCopy.SetColor("_EmissionColor", thisEmissionColor);
                    Transform[] lightParts = light.transform.parent.GetComponentsInChildren<Transform>();
                    foreach (Transform trans in lightParts)
                    {
                        if (transform.gameObject.name == "pipe")
                        {
                            trans.gameObject.GetComponent<Material>().color = thisEmissionColor;
                        }
                    }
                }

                
                //yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));

            while (lightIntensity < randomMax)
            {
                if (powerOff == true)
                {
                    PowerOff();
                    yield break;
                }

                lightIntensity += randomRiseRate;

                foreach (Light light in affectedLights)
                {
                    yield return new WaitForSeconds(Random.Range(0.01f, 0.03f));
                    light.intensity = lightIntensity * ceilingLampIntensity;
                    
                    thisEmissionColor = new Color(lightIntensity, lightIntensity, lightIntensity);
                    lightEmitterMatCopy.SetColor("_EmissionColor", thisEmissionColor);
                    Transform[] lightParts = light.transform.parent.GetComponentsInChildren<Transform>();
                    foreach (Transform trans in lightParts)
                    {
                        if (transform.gameObject.name == "pipe")
                        {
                            trans.gameObject.GetComponent<Material>().color = thisEmissionColor;
                        }
                    }
                }

                
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(Random.Range(0.00f, 1.3f));
            counter++;
        }


       
    }

    IEnumerator FastPowerFluctuation()
    {
        //print("fast fluctuations in power");
        int numberOfFluctuations = Random.Range(1,3);
        powerFluctuating = true;
        float lightIntensity = 1;
        int counter = 0;
        int lightCounter = 0;


        while (counter < numberOfFluctuations)
        {
            float randomRiseRate = Random.Range(0.7f,1.0f);
            float randomFallRate = Random.Range(0.7f, 1.0f);
            float randomMax = Random.Range(0.3f,1f);
            float randomMin = Random.Range(0.0f, 0.7f);


            while (lightIntensity > randomMin)
            {
                if (powerOff == true)
                {
                    PowerOff();
                    yield break;
                }

                lightIntensity -= randomFallRate;

                foreach (Light light in allPoweredLights)
                {
                    light.intensity = allPoweredLightIntensities[lightCounter] * lightIntensity;
                    lightCounter++;
                    emissionColor = new Color(lightIntensity, lightIntensity, lightIntensity);
                    lightEmitterMat.SetColor("_EmissionColor", emissionColor);
                }

                lightCounter = 0;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(Random.Range(0.01f,0.03f));

            while (lightIntensity < randomMax)
            {
                if (powerOff == true)
                {
                    PowerOff();
                    yield break;
                }

                lightIntensity += randomRiseRate;

                foreach (Light light in allPoweredLights)
                {
                    light.intensity = allPoweredLightIntensities[lightCounter] * lightIntensity;
                    lightCounter++;
                    emissionColor = new Color(lightIntensity, lightIntensity, lightIntensity);
                    lightEmitterMat.SetColor("_EmissionColor", emissionColor);
                }

                lightCounter = 0;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(Random.Range(0.01f, 0.03f));
            counter++;
        }


        powerFluctuating = false;
    }

    IEnumerator SlowPowerFluctuation()
    {
        //print("slow fluctuations in power");
        powerFluctuating = true;
        float lightIntensity = 1;
        int lightCounter = 0;
        float randomRiseRate = Random.Range(0.02f, 0.04f);
        float randomFallRate = Random.Range(0.02f, 0.04f);
        float randomMin = Random.Range(0.0f, 0.7f);

        while (lightIntensity > randomMin)
        {
            if (powerOff == true)
            {
                PowerOff();
                yield break;
            }

            lightIntensity -= randomFallRate;

            foreach (Light light in allPoweredLights)
            {
                light.intensity = allPoweredLightIntensities[lightCounter] * lightIntensity;
                lightCounter++;
                emissionColor = new Color(lightIntensity, lightIntensity, lightIntensity);
                lightEmitterMat.SetColor("_EmissionColor", emissionColor);
            }

            lightCounter = 0;
            yield return new WaitForSeconds(0.05f);
        }

        

        while (lightIntensity < 1)
        {
            if (powerOff == true)
            {
                PowerOff();
                yield break;
            }

            lightIntensity += randomRiseRate;

            foreach (Light light in allPoweredLights)
            {
                light.intensity = allPoweredLightIntensities[lightCounter] * lightIntensity;
                lightCounter++;
                emissionColor = new Color(lightIntensity, lightIntensity, lightIntensity);
                lightEmitterMat.SetColor("_EmissionColor", emissionColor);
            }

            lightCounter = 0;
            yield return new WaitForSeconds(0.05f);
        }

        powerFluctuating = false;
    }


    void PowerOff() 
    {
        print("power off");
        powerOff = true;
        playerClick.isThereAnyPower = false;
        playerClick.PowerWentOff();
        

        foreach (Light light in allPoweredLights)
        {
            light.intensity = 0;
        }

        StartCoroutine("FadeOutLightEmitterMat");
    }

    void PowerOn() 
    {
        powerOff = false;
        playerClick.isThereAnyPower = true;
    }

    IEnumerator FadeOutLightEmitterMat() //fader emitter-materialet på lys langsomt ned
    {
        emissionColor = emissionColor * 0.333f;
        lightEmitterMat.SetColor("_EmissionColor", emissionColor);
        

        while (lightEmitterMat.GetColor("_EmissionColor").r > 0.001 || lightEmitterMat.GetColor("_EmissionColor").g > 0.001 || lightEmitterMat.GetColor("_EmissionColor").b > 0.001)
        {
            emissionColor = emissionColor * 0.995f;
            lightEmitterMat.SetColor("_EmissionColor", emissionColor);
            lightEmitterMatCopy.SetColor("_EmissionColor", emissionColor);

            /*
            print("r");
            print(lightEmitterMat.GetColor("_EmissionColor").r);
            print("g");
            print(lightEmitterMat.GetColor("_EmissionColor").g);
            print("b");
            print(lightEmitterMat.GetColor("_EmissionColor").b);
            */


            yield return new WaitForSecondsRealtime(0.08f);
        }

        emissionColor = new Color (0,0,0);
        lightEmitterMat.SetColor("_EmissionColor", emissionColor);
        lightEmitterMatCopy.SetColor("_EmissionColor", emissionColor);

    }
}
