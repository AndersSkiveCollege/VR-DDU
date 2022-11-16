using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;



public class PlayerClick : MonoBehaviour // På Playeren
{
    public Material[] materials;
    public Renderer[] screens;
    public Camera[] cameras;
   // public List<Camera> enabledCameras = new List<Camera>();
   // public Queue<Camera> queueofEnabledCameras = new Queue<Camera>();
    public Light[] screenLights;
    //public int[] numberOfTimesCamerasAreUsed;
    public bool screen1On;
    public bool screen2On;
    public bool screen3On;
    public bool noCamSelectedScreen1;
    public bool noCamSelectedScreen2;
    public bool noCamSelectedScreen3;
    public Material screen1Mat;
    public Material screen2Mat;
    public Material screen3Mat;
    public Material noSignal;
    public bool isThereAnyPower;



    Ray ray;
    //public GameObject door;



    DoorController doorController;

    private void Awake()
    {
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }
    }

    void Start()
    {
        isThereAnyPower = true;
        doorController = GetComponent<DoorController>();
        noCamSelectedScreen1 = true;
        noCamSelectedScreen2 = true;
        noCamSelectedScreen3 = true;


    }



    void OnClick()
    {
        //print("clicked");

        if (isThereAnyPower == false)
        {
            print("You clicked but there is no power so nothing happens");
            return;
        }

        RaycastHit hit;
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Button")
            {
                int digit1 = hit.collider.gameObject.name[6] - '0';
                int digit2 = hit.collider.gameObject.name[7] - '0';
                int doorNr = int.Parse(digit1.ToString() + digit2.ToString());
                doorController.ToggleDoor(doorNr);
                //door.SetActive(false);
            }
        }

        switch (hit.collider.gameObject.name)
        {
            case "KnapS1K1":
                print("clicked button 1");
                TurnSelectedCamerasOnAndDisableTheRest(0,0,1);
                break;

            case "KnapS1K2":
                TurnSelectedCamerasOnAndDisableTheRest(1, 0, 2);
                break;

            case "KnapS1K3":
                TurnSelectedCamerasOnAndDisableTheRest(2, 0, 3);
                break;

            case "KnapS1K4":
                TurnSelectedCamerasOnAndDisableTheRest(3, 0, 4);
                break;

            case "KnapS1K5":
                TurnSelectedCamerasOnAndDisableTheRest(4, 0, 5);
                break;

            case "KnapS1K6":
                TurnSelectedCamerasOnAndDisableTheRest(5, 0, 6);
                break;

            case "KnapS1K7":
                TurnSelectedCamerasOnAndDisableTheRest(6, 0, 7);
                break;

            case "KnapS1K8":
                TurnSelectedCamerasOnAndDisableTheRest(7, 0, 8);
                break;

            case "KnapS1K9":
                TurnSelectedCamerasOnAndDisableTheRest(8, 0, 9);
                break;

            case "KnapS1K10":
                TurnSelectedCamerasOnAndDisableTheRest(9, 0, 10);
                break;

            case "KnapS1K11":
                TurnSelectedCamerasOnAndDisableTheRest(10, 0, 11);
                break;

            case "KnapS2K1":
                TurnSelectedCamerasOnAndDisableTheRest(0, 1, 1);
                break;

            case "KnapS2K2":
                TurnSelectedCamerasOnAndDisableTheRest(1, 1, 2);
                break;

            case "KnapS2K3":
                TurnSelectedCamerasOnAndDisableTheRest(2, 1, 3);
                break;

            case "KnapS2K4":
                TurnSelectedCamerasOnAndDisableTheRest(3, 1, 4);
                break;

            case "KnapS2K5":
                TurnSelectedCamerasOnAndDisableTheRest(4, 1, 5);
                break;

            case "KnapS2K6":
                TurnSelectedCamerasOnAndDisableTheRest(5, 1, 6);
                break;

            case "KnapS2K7":
                TurnSelectedCamerasOnAndDisableTheRest(6, 1, 7);
                break;

            case "KnapS2K8":
                TurnSelectedCamerasOnAndDisableTheRest(7, 1, 8);
                break;

            case "KnapS2K9":
                TurnSelectedCamerasOnAndDisableTheRest(8, 1, 9);
                break;

            case "KnapS2K10":
                TurnSelectedCamerasOnAndDisableTheRest(9, 1, 10);
                break;

            case "KnapS2K11":
                TurnSelectedCamerasOnAndDisableTheRest(10, 1, 11);
                break;

            case "KnapS3K1":
                TurnSelectedCamerasOnAndDisableTheRest(0, 2, 1);
                break;

            case "KnapS3K2":
                TurnSelectedCamerasOnAndDisableTheRest(1, 2, 2);
                break;

            case "KnapS3K3":
                TurnSelectedCamerasOnAndDisableTheRest(2, 2, 3);
                break;

            case "KnapS3K4":
                TurnSelectedCamerasOnAndDisableTheRest(3, 2, 4);
                break;

            case "KnapS3K5":
                TurnSelectedCamerasOnAndDisableTheRest(4, 2, 5);
                break;

            case "KnapS3K6":
                TurnSelectedCamerasOnAndDisableTheRest(5, 2, 6);
                break;

            case "KnapS3K7":
                TurnSelectedCamerasOnAndDisableTheRest(6, 2, 7);
                break;

            case "KnapS3K8":
                TurnSelectedCamerasOnAndDisableTheRest(7, 2, 8);
                break;

            case "KnapS3K9":
                TurnSelectedCamerasOnAndDisableTheRest(8, 2, 9);
                break;

            case "KnapS3K10":
                TurnSelectedCamerasOnAndDisableTheRest(9, 2, 10);
                break;

            case "KnapS3K11":
                TurnSelectedCamerasOnAndDisableTheRest(10, 2, 11);
                break;

            case "KnapS1Sluk":
                TurnSelectedCamerasOnAndDisableTheRest(-1, 0, 0);
                break;

            case "KnapS2Sluk":
                TurnSelectedCamerasOnAndDisableTheRest(-1, 1, 0);
                break;

            case "KnapS3Sluk":
                TurnSelectedCamerasOnAndDisableTheRest(-1, 2, 0);
                break;

            default:
                break;
        }

    }

    //Needs to let screens remember which camera they are showing to fix issue

   public void TurnSelectedCamerasOnAndDisableTheRest(int camera, int screen, int mat) // Takes the camera, screen and material and turns off all unused cameras.
   {
        if (camera == -1)   //for turning off screens
        {
            if (screen == 0)
            {
                if (screen1On == true)
                {
                    print("screenOn");
                    screen1On = false;
                    screenLights[screen].enabled = false;
                    //screen1Mat = materials[0];
                    screens[screen].material.CopyPropertiesFromMaterial(materials[0]);
                    return;
                }

                else
                {
                    screen1On = true;
                    screenLights[screen].enabled = true;
                    if (noCamSelectedScreen1 == true)
                    {
                        print("Screen Off NoCamSelected");
                        screens[screen].material.CopyPropertiesFromMaterial(noSignal);
                    }
                    else
                    {
                        print("Screen Off CamSelected");
                        screens[screen].material.CopyPropertiesFromMaterial(screen1Mat);
                    }
                    
                }
            }

            if (screen == 1)
            {
                if (screen2On == true)
                {
                    screen2On = false;
                    screenLights[screen].enabled = false;
                    //screen2Mat = materials[0];
                    screens[screen].material.CopyPropertiesFromMaterial(materials[0]);
                    return;
                }

                else
                {
                    screen2On = true;
                    screenLights[screen].enabled = true;
                    if (noCamSelectedScreen2 == true)
                    {
                        screens[screen].material.CopyPropertiesFromMaterial(noSignal);
                    }
                    else
                    {
                        screens[screen].material.CopyPropertiesFromMaterial(screen2Mat);
                    }
                    
                }
            }

            if (screen == 2)
            {
                if (screen3On == true)
                {
                    screen3On = false;
                    screenLights[screen].enabled = false;
                    //screen3Mat = materials[0];
                    screens[screen].material.CopyPropertiesFromMaterial(materials[0]);
                    return;
                }

                else
                {

                    screen3On = true;
                    screenLights[screen].enabled = true;
                    if (noCamSelectedScreen3 == true)
                    {
                        screens[screen].material.CopyPropertiesFromMaterial(noSignal);
                    }
                    else
                    {
                        screens[screen].material.CopyPropertiesFromMaterial(screen3Mat);
                    }
                    
                }
            }

            
            

            return;
        }

        else  // saving used materials
        {
            if (screen == 0)
            {
                print("screen turning on");
                noCamSelectedScreen1 = false;
                screen1On = true;
                screen1Mat = materials[mat];
            }

            if (screen == 1)
            {
                noCamSelectedScreen2 = false;
                screen2On = true;
                screen2Mat = materials[mat];
            }

            if (screen == 2)
            {
                noCamSelectedScreen3 = false;
                screen3On = true;
                screen3Mat = materials[mat];
            }

        }

        for (int i = 0; i < materials.Length; i++)  //turn off all cameras that arent used
        {

            if (materials[i] == screen1Mat || materials[i] == screen2Mat || materials[i] == screen3Mat)
            {
                if (i != 0)
                {
                    print(i);
                    cameras[i - 1].enabled = true;
                }

                
            }

            else
            {
                if (i != 0)
                {
                    cameras[i - 1].enabled = false;
                }
                
            }
        }

        screenLights[screen].enabled = true;
        screens[screen].material.CopyPropertiesFromMaterial(materials[mat]);


        
   }

    public void PowerWentOff()
    {
        screen1On = false;
        screen2On = false;
        screen3On = false;
        screenLights[0].enabled = false;
        screenLights[1].enabled = false;
        screenLights[2].enabled = false;
        screens[0].material.CopyPropertiesFromMaterial(materials[0]);
        screens[1].material.CopyPropertiesFromMaterial(materials[0]);
        screens[2].material.CopyPropertiesFromMaterial(materials[0]);
    }
}