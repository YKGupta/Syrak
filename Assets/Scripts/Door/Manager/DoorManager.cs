using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Transform doorsParent;
    public DoorFunctions doorFunctions;

    public event Action penaltyIncurred;
    public event Action doorOpened;

    private Door currentDoor;

    private int totalDoors;
    private int openedDoors;

    private int currentDoorIndex = 0;
    private List<Door> doors;

    [HideInInspector]
    public bool doorIsActive;

    private void Awake()
    {
        doors = new List<Door>();
        totalDoors = doorsParent.childCount;
        openedDoors = 0;
        currentDoorIndex = 0;
        for(int i = 0; i < doorsParent.childCount; i++)
        {
            Door door = doorsParent.GetChild(i).GetComponent<Door>();
            door.enabled = false;
            if(door.index == currentDoorIndex)
                door.enabled = true;
            door.PlayerEntered += OnDoorEntered;
            door.PlayerExited += OnDoorExited;
            doors.Add(door);
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
            StartCoroutine(RemoveQuestionAndOpenDoor(currentDoor.doorQuestion.mode == QuestionPresentationMode.Key ? 0f : 2f));
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

        doorOpened?.Invoke();
        
        currentDoor = null;
        openedDoors++;

        currentDoorIndex++;
        SetNextDoor();

    }

    private void SetNextDoor()
    {
        Door door = doors.Find(x => x.index == currentDoorIndex);
        if(door == null)
            return;
        door.enabled = true;
    }
}
