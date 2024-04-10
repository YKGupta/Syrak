using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public KeyCode doorInteractionKey;
    public GameObject InteractionInstructionsGO;
    public Transform doorsParent;
    public DoorFunctions doorFunctions;

    public event Action penaltyIncurred;
    public event Action doorOpened;

    private Door currentDoor;

    private int totalDoors;
    private int openedDoors;

    private int currentDoorIndex = 0;
    private List<Door> doors;

    private bool doorInteractionKeyPressed;

    [HideInInspector]
    public bool doorIsActive;

    private Door b_door;

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
        doorInteractionKeyPressed = Input.GetKeyDown(doorInteractionKey);
        
        if(doorInteractionKeyPressed && currentDoor == null && b_door != null)
        {
            SetDoor(b_door);
        }

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

    public void OnDoorEntered(Door door, Transform player, bool externallyTriggered = false)
    {
        if(externallyTriggered)
        {
            SetDoor(door);
            return;
        }

        if(!InteractionInstructionsGO.activeSelf)
        {
            InteractionInstructionsGO.SetActive(true);
        }

        b_door = door;
    }

    public void OnDoorExited(Door door, Transform player, bool exitedTrigger)
    {
        if(exitedTrigger)
        {
            b_door = null;
            InteractionInstructionsGO.SetActive(false);
        }
        else
            InteractionInstructionsGO.SetActive(true);


        currentDoor = null;
        doorFunctions.RemoveQuestion(door, this);
    }

    public void OnPenaltyIncurred()
    {
        SoundManager.instance.Play("Buzz");
        penaltyIncurred?.Invoke();
    }

    private IEnumerator RemoveQuestionAndOpenDoor(float waitTime)
    {
        doorFunctions.OpenDoor(currentDoor, this);
        yield return new WaitForSeconds(waitTime);
        doorFunctions.RemoveQuestion(currentDoor, this);

        doorOpened?.Invoke();
        
        currentDoor = null;
        b_door = null;
        openedDoors++;

        currentDoorIndex++;
        SetNextDoor();

    }

    private void SetDoor(Door door)
    {
        if(currentDoor == door)
            return;
        
        currentDoor = door;
        doorFunctions.PresentQuestion(door, this);
        InteractionInstructionsGO.SetActive(false);
    }

    private void SetNextDoor()
    {
        Door door = doors.Find(x => x.index == currentDoorIndex);
        if(door == null)
            return;
        door.enabled = true;
    }
}
