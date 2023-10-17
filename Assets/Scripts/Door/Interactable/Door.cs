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

    public event Action<Door, Transform> PlayerEntered; 
    public event Action<Door, Transform> PlayerExited; 

    private bool isDoorTriggered = false;

    private void Update()
    {
        if(!isDoorTriggered)
            return;

        if(Input.GetKeyDown(exitDoorKeyCode))
        {
            PlayerExited?.Invoke(this, null);
        }
    }

    public void ExternalEnterDoorInvokation(Transform other)
    {
        PlayerEntered?.Invoke(this, other);
        isDoorTriggered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!enabled)
            return;

        isDoorTriggered = true;
            
        if(other.CompareTag("Player"))
        {
            PlayerEntered?.Invoke(this, other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!enabled)
            return;

        isDoorTriggered = false;
            
        if(other.CompareTag("Player"))
        {
            PlayerExited?.Invoke(this, other.transform);
        }
    }
}
