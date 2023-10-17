using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ObjectInteractionManager : MonoBehaviour
{
    public Transform objectsParent;
    public Transform objectViewPoint;
    public Volume globalVolume;
    public ObjectViewer objectViewer;
    public KeyCode cancelInteractionKey;
    // public PauseManager pauseManager;

    public UnityEvent onViewObject;
    public UnityEvent onCloseObjectView;

    private DepthOfField depthOfField;
    private ObjectInteractable currentInteractable;

    private void Start()
    {
        for(int i = 0; i < objectsParent.childCount; i++)
        {
            objectsParent.GetChild(i).GetComponent<ObjectInteractable>().objectClicked += OnObjectClicked;
        }

        globalVolume.profile.TryGet<DepthOfField>(out depthOfField);
    }

    private void Update()
    {
        // if(pauseManager.isPaused)
        //     return;
            
        if(Input.GetKeyDown(cancelInteractionKey))
        {
            OnObjectViewCancel();
        }
    }

    public void OnObjectClicked(ObjectInteractable interactable)
    {
        if(!PlayerManager.instance.isPlayerAllowedToInteractWithObjects)
            return;

        // Player Manager is then set to stop player from opening inventory, interacting with objects, moving or looking in the unity event invoked later in the function

        objectViewer.SetTarget(interactable.mainObject.transform);
        
        interactable.mainObject.transform.position = objectViewPoint.position;
        interactable.mainObject.transform.rotation = objectViewPoint.rotation;
        
        interactable.ResetCanvas();
        interactable.enabled = false;

        depthOfField.mode.Override(DepthOfFieldMode.Bokeh);
        depthOfField.focusDistance.Override(1.7f);
        
        currentInteractable = interactable;

        onViewObject.Invoke();
    }

    public void OnObjectViewCancel()
    {
        if(currentInteractable == null)
            return;

        currentInteractable.enabled = true;
        currentInteractable.mainObject.transform.position = currentInteractable.initialPositionOfMainObject; 
        currentInteractable.mainObject.transform.rotation = currentInteractable.initialRotationOfMainObject; 

        depthOfField.mode.Override(DepthOfFieldMode.Off);

        currentInteractable = null;

        onCloseObjectView.Invoke();

        objectViewer.ResetTargetProperties();
        objectViewer.SetTarget(null);
    }
}
