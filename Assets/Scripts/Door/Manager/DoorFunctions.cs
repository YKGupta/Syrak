using UnityEngine;

public class DoorFunctions : MonoBehaviour
{
    public void PresentQuestion(Door door, DoorManager doorManager)
    {
        DoorQuestion temp = door.doorQuestion;
        doorManager.doorIsActive = true;
        switch(temp.mode)
        {
            case QuestionPresentationMode.Text:
            {
                temp.questionText.text = temp.question.question;
                temp.textCanvasGO.SetActive(true);
                break;
            }
            
            case QuestionPresentationMode.Audio:
            {
                temp.source.clip = temp.clip;
                temp.source.Play();
                break;
            }
            
            case QuestionPresentationMode.Visual:
            {
                temp.animator.SetTrigger(temp.trigger);
                break;
            }
            
            default: 
                RemoveQuestion(door, doorManager);
                Debug.LogError("No valid mode of question presentation specified.");
                return;
        }
    }

    public void RemoveQuestion(Door door, DoorManager doorManager)
    {
        DoorQuestion temp = door.doorQuestion;
        doorManager.doorIsActive = false;
        switch(temp.mode)
        {
            case QuestionPresentationMode.Text:
            {
                temp.questionText.text = "";
                temp.textCanvasGO.SetActive(false);
                break;
            }
            
            case QuestionPresentationMode.Audio:
            {
                temp.source.Stop();
                break;
            }
            
            case QuestionPresentationMode.Visual:
            {
                temp.animator.ResetTrigger(temp.trigger);
                break;
            }
            
            default: 
                Debug.LogError("No valid mode of question presentation specified.");
                break;
        }
    }

    public bool ActiveDoor(Door door)
    {
        if(door.doorQuestion.mode != QuestionPresentationMode.Text)
            return false;
        
        foreach(Option op in door.doorQuestion.question.options)
        {
            if(Input.GetKeyDown(op.keyCode))
            {
                if(op.isCorrect)
                {
                    Debug.Log("Correct Answer!!");
                    door.enabled = false;
                    return true;
                }
                else
                {
                    Debug.Log("Wrong answer :(");
                }
            }
        }
        return false;
    }

    public void OpenDoor(Door door, DoorManager doorManager)
    {
        door.animator.SetTrigger("Open");
    }
}
