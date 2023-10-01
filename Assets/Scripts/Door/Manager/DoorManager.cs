using System;
using System.Collections;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Transform doorsParent;
    public DoorFunctions doorFunctions;

    public event Action penaltyIncurred;

    private Door currentDoor;

    private int totalDoors;
    private int openedDoors;

    [HideInInspector]
    public bool doorIsActive;

    private void Awake()
    {
        totalDoors = doorsParent.childCount;
        openedDoors = 0;
        for(int i = 0; i < doorsParent.childCount; i++)
        {
            Door door = doorsParent.GetChild(i).GetComponent<Door>();
            door.PlayerEntered += OnDoorEntered;
            door.PlayerExited += OnDoorExited;
        }
        currentDoor = null;
    }

    private void Update()
    {
        if(!doorIsActive)
            return;
        
        bool result = doorFunctions.ActiveDoor(currentDoor, this);
        if(result)
        {
            StartCoroutine(RemoveQuestionAndOpenDoor(2f));
        }
    }

    public float GetProgress()
    {
        return (float)openedDoors/totalDoors;
    }

    public int GetTotalDoors()
    {
        return totalDoors;
    }

    public int GetOpenedDoorsNumber()
    {
        return openedDoors;
    }

    public void OnDoorEntered(Door door, Transform player)
    {
        currentDoor = door;
        doorFunctions.PresentQuestion(door, this);
    }

    public void OnDoorExited(Door door, Transform player)
    {
        currentDoor = null;
        doorFunctions.RemoveQuestion(door, this);
    }

    public void OnPenaltyIncurred()
    {
        penaltyIncurred?.Invoke();
    }

    private IEnumerator RemoveQuestionAndOpenDoor(float waitTime)
    {
        doorFunctions.OpenDoor(currentDoor, this);
        yield return new WaitForSeconds(waitTime);
        doorFunctions.RemoveQuestion(currentDoor, this);
        currentDoor = null;

        openedDoors++;
    }
}
