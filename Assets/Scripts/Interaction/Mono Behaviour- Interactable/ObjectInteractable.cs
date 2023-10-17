using System;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    public GameObject objectInteractionInstructiosPanel;
    public GameObject mainObject;
    public ObjectSO obj;
    // public PauseManager pauseManager;

    [HideInInspector]
    public Vector3 initialPositionOfMainObject;
    [HideInInspector]
    public Quaternion initialRotationOfMainObject;

    // Events
    public Action<ObjectInteractable> objectClicked;

    private void Start()
    {
        initialPositionOfMainObject = mainObject.transform.position;
        initialRotationOfMainObject = mainObject.transform.rotation;
    }

    private void OnMouseEnter()
    {
        if(!enabled /*|| pauseManager.isPaused*/)
            return;

        objectInteractionInstructiosPanel.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(!enabled  /*|| pauseManager.isPaused*/)
            return;

        if(objectClicked != null)
            objectClicked(this);
    }

    private void OnMouseUp()
    {
        if(!enabled  /*|| pauseManager.isPaused*/)
            return;

    }

    private void OnMouseExit()
    {
        if(!enabled  /*|| pauseManager.isPaused*/)
            return;

        objectInteractionInstructiosPanel.SetActive(false);
    }

    public void ResetCanvas()
    {
        objectInteractionInstructiosPanel.SetActive(false);
    }
}
