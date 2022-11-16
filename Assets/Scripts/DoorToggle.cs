using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public enum DoorState
{
    Closed,
    IsAnimating,
    Open,
}



public enum DoorType
{
    Swing,
    Slide,
}



public class DoorToggle : MonoBehaviour // På døren
{
    public GameObject hinge;
    public GameObject gameModel;



    public int id;
    public DoorType type;
    public DoorState state;
    public Animation anim;
    public Vector3 startPosition;



    public bool doThing;



    private void Start()
    {
        startPosition = gameModel.transform.localPosition;

    }



    private void Update()
    {
        if (doThing)
        {
            doThing = false;
            Toggle();
        }
    }



    public void Toggle()
    {
        if (type == DoorType.Swing)
        {
            ToggleSwingDoor();
            return;
        }



        ToggleSlideDoor();
        return;
    }



    public void ToggleSwingDoor()
    {
        switch (state)
        {
            case DoorState.Closed:
                anim.Play("SwingDoorAnimOpen");
                state = DoorState.Open;
                break;



            case DoorState.Open:
                anim.Play("SwingDoorAnimClose");
                state = DoorState.Closed;
                break;
        }
    }



    public void ToggleSlideDoor()
    {



    }
}