using UnityEngine;

public class Computer : MonoBehaviour
{
    public TriggerEvents triggerEvents;
    public DoorFunctions doorFunctions;

    private Door door;
    private bool prevDoorQuestionState;

    private void Start()
    {
        triggerEvents.onTriggerEnter += OnPlayerEnter;
        doorFunctions.doorInteractionOver += OnDoorInteractionStart;
        doorFunctions.doorInteractionOver += OnDoorInteractionOver;
    }

    public void OnPlayerEnter(Collider playerC)
    {
        if(door == null || !playerC.CompareTag("Player"))
            return;
        
        prevDoorQuestionState = door.doorQuestion.showQuestion;
        door.doorQuestion.showQuestion = true;

        door.ExternalEnterDoorInvokation(playerC.transform);
    }

    public void OnDoorInteractionOver(Door door)
    {
        if(this.door == null || door.index != this.door.index)
            return;
        
        door.doorQuestion.showQuestion = prevDoorQuestionState;
        if(!door.enabled)
            this.door = null;
    }

    public void OnDoorInteractionStart(Door door)
    {
        if(door.doorQuestion.showQuestion || door.doorQuestion.mode == QuestionPresentationMode.Key)
            return;
        
        this.door = door;
    }
}
