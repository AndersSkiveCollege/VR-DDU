using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraVolumeController : MonoBehaviour
{
    public Volume Volume;

    // Start is called before the first frame update
    void Start()
    {

        Volume.weight = 1;

    }

}
