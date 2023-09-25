using System.Collections;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Transform doorsParent;
    public DoorFunctions doorFunctions;

    private Door currentDoor;

    [HideInInspector]
    public bool doorIsActive;

    private void Awake()
    {
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
        
        bool result = doorFunctions.ActiveDoor(currentDoor);
        if(result)
        {
            StartCoroutine(RemoveQuestionAndOpenDoor(2f));
        }
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

    private IEnumerator RemoveQuestionAndOpenDoor(float waitTime)
    {
        doorFunctions.OpenDoor(currentDoor, this);
        yield return new WaitForSeconds(waitTime);
        doorFunctions.RemoveQuestion(currentDoor, this);
        currentDoor = null;
    }
}
