using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DoorController : MonoBehaviour // På Playeren
{
    //public static List<DoorToggle> doors = new List<DoorToggle>();
    public static Dictionary<int, DoorToggle> doors;
    private void Start()
    {
        GameObject[] doorObjs = GameObject.FindGameObjectsWithTag("Door");
        doors = new Dictionary<int, DoorToggle>();
        for (int i = 0; i < doorObjs.Length; i++)
        {
            var obj = doorObjs[i];

            var door = obj.GetComponent<DoorToggle>();
            int digit1 = door.name[11] - '0';
            int digit2 = door.name[12] - '0';
            int bothDigits = int.Parse(digit1.ToString() + digit2.ToString());

            door.id = bothDigits;

            if (door != null)
                doors[door.id] = door;
        }
    }



    public static DoorState GetDoorState(int id)
    {
        if (doors.Count <= 0)
        {
            Debug.LogError("[GetDoorState] No Doors found! This isnt what we signed up for. Please remember to tag each door.");
            return DoorState.Closed;
        }
        if (id < 0 || id >= doors.Count)
        {
            Debug.LogError("[GetDoorState] Id out of bound. Did you remember it was 0 indexed?");
            return DoorState.Closed;
        }

        return doors[id].state;
    }



    public void ToggleDoor(int id)
    {
        if (doors.Count <= 0)
        {
            Debug.LogError("[ToggleDoor] No Doors found! This isnt what we signed up for. Please remember to tag each door.");
            return;
        }
        if (id < 0 || id >= doors.Count)
        {
            Debug.LogError("[ToggleDoor] Id out of bound. Did you remember it was 0 indexed?");
            return;
        }

        GetDoor(id).Toggle();
    }



    public DoorToggle GetDoor(int id)
    {
        if (doors.TryGetValue(id, out var door))
        {
            return door;
        }

        return null;
    }
}