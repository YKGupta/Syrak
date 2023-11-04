using System;
using UnityEngine;
using NaughtyAttributes;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public Animator animator;
    [ShowIf("isLocked")]
    public int index;
    [ShowIf("isLocked")]
    public DoorQuestion doorQuestion;
    [ShowIf("isLocked")]
    public KeyCode exitDoorKeyCode;

    public event Action<Door, Transform, bool> PlayerEntered; 
    public event Action<Door, Transform, bool> PlayerExited; 

    private bool isDoorTriggered = false;
    private bool isComputerTriggered = false;

    private void Update()
    {
        if(!isDoorTriggered)
            return;

        if(Input.GetKeyDown(exitDoorKeyCode))
        {
            PlayerExited?.Invoke(this, null, isComputerTriggered);
        }
    }

    public void ExternalEnterDoorInvokation(Transform other)
    {
        PlayerEntered?.Invoke(this, other, true);
        isDoorTriggered = true;
        isComputerTriggered = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!enabled)
            return;

        isDoorTriggered = true;
            
        if(other.CompareTag("Player"))
        {
            PlayerEntered?.Invoke(this, other.transform, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!enabled)
            return;

        isDoorTriggered = false;
            
        if(other.CompareTag("Player"))
        {
            PlayerExited?.Invoke(this, other.transform, true);
        }
    }
}
