using UnityEngine;

public class Computer : MonoBehaviour
{
    public TriggerEvents triggerEvents;
    public DoorFunctions doorFunctions;
    public KeyCode computerInteractionKey;
    public GameObject InteractionInstructionsGO;

    private Door door;
    private bool prevDoorQuestionState;
    private bool isTriggered;

    private void Start()
    {
        triggerEvents.onTriggerEnter += OnPlayerEnter;
        triggerEvents.onTriggerStay += OnPlayerStay;
        triggerEvents.onTriggerExit += OnPlayerExit;
        doorFunctions.doorInteractionStart += OnDoorInteractionStart;
        doorFunctions.doorInteractionOver += OnDoorInteractionOver;
        isTriggered = false;
    }

    public void OnPlayerEnter(Collider playerC)
    {
        if(door == null || !playerC.CompareTag("Player"))
            return;

        isTriggered = true;
        InteractionInstructionsGO.SetActive(true);
    }

    public void OnPlayerStay(Collider playerC)
    {
        if(door == null || !playerC.CompareTag("Player"))
            return;

        if(!Input.GetKeyDown(computerInteractionKey))
            return;

        InteractionInstructionsGO.SetActive(false);

        prevDoorQuestionState = false;
        door.doorQuestion.showQuestion = true;

        door.ExternalEnterDoorInvokation(playerC.transform);
    }

    public void OnPlayerExit(Collider playerC)
    {
        if(!playerC.CompareTag("Player"))
            return;

        isTriggered = false;
        InteractionInstructionsGO.SetActive(false);
    }

    public void OnDoorInteractionOver(Door door)
    {
        if(this.door == null || door.index != this.door.index)
            return;
            
        door.doorQuestion.showQuestion = prevDoorQuestionState;
        if(!door.enabled)
            this.door = null;
            
        if(this.door != null && isTriggered)
            InteractionInstructionsGO.SetActive(true);
    }

    public void OnDoorInteractionStart(Door door)
    {
        if(door.doorQuestion.showQuestion || door.doorQuestion.mode == QuestionPresentationMode.Key)
            return;
        
        InteractionInstructionsGO.SetActive(false);
        
        this.door = door;
    }
}
