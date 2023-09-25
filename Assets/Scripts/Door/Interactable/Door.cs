using System;
using UnityEngine;
using NaughtyAttributes;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public Animator animator;
    [ShowIf("isLocked")]
    public DoorQuestion doorQuestion;

    public event Action<Door, Transform> PlayerEntered; 
    public event Action<Door, Transform> PlayerExited; 

    private void OnTriggerEnter(Collider other)
    {
        if(!enabled)
            return;
            
        if(other.CompareTag("Player"))
        {
            PlayerEntered?.Invoke(this, other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!enabled)
            return;
            
        if(other.CompareTag("Player"))
        {
            PlayerExited?.Invoke(this, other.transform);
        }
    }
}