using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorFunctions : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public event Action<Door> doorInteractionStart;
    public event Action<Door> doorInteractionOver;

    public void PresentQuestion(Door door, DoorManager doorManager)
    {
        if(door == null)
            return;

        doorInteractionStart?.Invoke(door);

        DoorQuestion temp = door.doorQuestion;
        doorManager.doorIsActive = true;

        PlayerManager.instance.SetPlayerState(false);

        switch(temp.mode)
        {
            case QuestionPresentationMode.MCQ:
            {
                if(temp.showQuestion)
                {
                    GameObject questionGO = Instantiate(temp.questionPrefab, temp.panelGO.transform);
                    questionGO.GetComponent<UI_Initialiser>().SetText(temp.question.question);
                    
                    foreach(Option op in temp.question.options)
                    {
                        GameObject optionGO = Instantiate(temp.optionPrefab, temp.panelGO.transform);
                        optionGO.GetComponent<UI_Initialiser>().SetText(op.option);
                        op.associatedGO = optionGO;
                    }
                }

                temp.textCanvasGO.SetActive(true);
                temp.panelGO.SetActive(true);
                break;
            }

            case QuestionPresentationMode.Key:
            {
                if(temp.showQuestion)
                {
                    GameObject questionGO = Instantiate(temp.keyQuestionPrefab, temp.keyPanelGO.transform);
                    questionGO.GetComponent<UI_Initialiser>().SetText(temp.keyQuestion.question);
                    temp.keyQuestion.associatedGO = questionGO;
                }

                temp.keyCanvasGO.SetActive(true);
                temp.keyPanelGO.SetActive(true);
                temp.keyTestInstructionsGO.SetActive(true);
                break;
            }

            case QuestionPresentationMode.Subjective:
            {
                if(temp.showQuestion)
                {
                    Cursor.lockState = CursorLockMode.None;

                    GameObject questionGO = Instantiate(temp.subjectiveQuestionPrefab, temp.subjectivePanelGO.transform);
                    GameObject answerGO = Instantiate(temp.subjectiveAnswerPrefab, temp.subjectivePanelGO.transform);
                    questionGO.GetComponent<UI_Initialiser>().SetText(temp.subjectiveQuestion.question);
                    temp.subjectiveQuestion.associatedGO = questionGO;
                    temp.subjectiveQuestion.answer.associatedGO = answerGO;
                    temp.subjectiveQuestion.answer.associatedGO.GetComponent<UI_Initialiser>().GetInputField().onEndEdit.AddListener(temp.subjectiveQuestion.answer.OnEndEdit);
                }

                temp.subjectivePanelGO.SetActive(true);
                temp.subjectiveCanvasGO.SetActive(true);
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
        if(door == null)
            return;

        DoorQuestion temp = door.doorQuestion;
        doorManager.doorIsActive = false;

        PlayerManager.instance.SetPlayerState(true);
        
        switch(temp.mode)
        {
            case QuestionPresentationMode.MCQ:
            {
                if(temp.showQuestion)
                {
                    for(int i = 0; i < temp.panelGO.transform.childCount; i++)
                        Destroy(temp.panelGO.transform.GetChild(i).gameObject);
                }

                temp.textCanvasGO.SetActive(false);
                temp.panelGO.SetActive(false);
                break;
            }
        
            case QuestionPresentationMode.Key:
            {
                if(temp.showQuestion)
                {
                    for(int i = 0; i < temp.keyPanelGO.transform.childCount; i++)
                        Destroy(temp.keyPanelGO.transform.GetChild(i).gameObject);
                }

                temp.keyCanvasGO.SetActive(false);
                temp.keyCanvasGO.SetActive(false);
                temp.keyTestInstructionsGO.SetActive(false);
                break;
            }
        
            case QuestionPresentationMode.Subjective:
            {                    
                Cursor.lockState = CursorLockMode.Locked;
                if(temp.showQuestion)
                {
                    for(int i = 0; i < temp.subjectivePanelGO.transform.childCount; i++)
                        Destroy(temp.subjectivePanelGO.transform.GetChild(i).gameObject);
                }            

                temp.subjectivePanelGO.SetActive(false);
                temp.subjectiveCanvasGO.SetActive(false);
                break;
            }
            
            default: 
                Debug.LogError("No valid mode of question presentation specified.");
                break;
        }

        doorInteractionOver?.Invoke(door);
    }

    public bool ActiveDoor(Door door, DoorManager doorManager)
    {
        if(door.doorQuestion.mode != QuestionPresentationMode.MCQ && door.doorQuestion.mode != QuestionPresentationMode.Key && door.doorQuestion.mode != QuestionPresentationMode.Subjective)
            return false;

        switch(door.doorQuestion.mode)
        {
            case QuestionPresentationMode.MCQ:
            {
                if(!door.doorQuestion.showQuestion)
                    break;
        
                foreach(Option op in door.doorQuestion.question.options)
                {
                    if(!Input.GetKeyDown(op.keyCode))
                        continue;

                    if(op.isCorrect)
                    {
                        StartCoroutine(FlashColor(op.associatedGO.GetComponent<UI_Initialiser>().GetImage(), op.correctColor));
                        door.enabled = false;
                        return true;
                    }
                    else
                    {
                        StartCoroutine(FlashColor(op.associatedGO.GetComponent<UI_Initialiser>().GetImage(), op.incorrectColor));
                        doorManager.OnPenaltyIncurred();
                    }
                }
                break;
            }

            case QuestionPresentationMode.Key:
            {
                if(!Input.GetKeyDown(door.doorQuestion.keyQuestion.keyCode))
                    break;

                KeyQuestion keyQuestion = door.doorQuestion.keyQuestion;
                Item item = inventoryManager.FindItem(keyQuestion.itemId);
                if(item == null)
                {
                    if(door.doorQuestion.showQuestion)
                        StartCoroutine(FlashColor(keyQuestion.associatedGO.GetComponent<UI_Initialiser>().GetImage(), keyQuestion.incorrectColor));
                    else
                        StartCoroutine(FlashColor(door.doorQuestion.keyPanelGO.GetComponent<UI_Initialiser>().GetImage(), keyQuestion.incorrectColor));
                }
                else
                {
                    inventoryManager.RemoveItem(item, true, inventoryManager.inventoryGO.activeSelf);
                    door.enabled = false;

                    if(door.doorQuestion.showQuestion)
                        StartCoroutine(FlashColor(keyQuestion.associatedGO.GetComponent<UI_Initialiser>().GetImage(), keyQuestion.correctColor));

                    return true;
                }

                break;
            }

            case QuestionPresentationMode.Subjective:
            {
                if(!door.doorQuestion.showQuestion)
                    break;
                    
                SubjectiveQuestion subjectiveQuestion = door.doorQuestion.subjectiveQuestion;

                if(!subjectiveQuestion.answer.answered)
                    break;

                subjectiveQuestion.answer.answered = false;

                string typedAnswer = subjectiveQuestion.answer.associatedGO.GetComponent<UI_Initialiser>().GetInputField().text;

                if(!typedAnswer.ToLower().Equals(subjectiveQuestion.answer.answer.ToLower()))
                {
                    StartCoroutine(FlashColor(subjectiveQuestion.answer.associatedGO.GetComponent<UI_Initialiser>().GetImage(), subjectiveQuestion.incorrectColor));
                    doorManager.OnPenaltyIncurred();
                }
                else
                {
                    StartCoroutine(FlashColor(subjectiveQuestion.answer.associatedGO.GetComponent<UI_Initialiser>().GetImage(), subjectiveQuestion.correctColor));
                    door.enabled = false;
                    return true;
                }

                break;
            }

            default:
            {
                Debug.LogError("Invalid presentation mode for activation function!");
                break;
            }
        }
        
        return false;
    }

    public void OpenDoor(Door door, DoorManager doorManager)
    {
        door.animator.SetTrigger("Open");
        SoundManager.instance.Play("Door");
    }

    private IEnumerator FlashColor(Image imageToFlash, Color flashColor, float frequency = 2, float timePerFlash = 0.2f)
    {
        Color baseColor = imageToFlash.color;
        for(int i = 0; i < frequency; i++)
        {
            float startTime = Time.time;
            while(Time.time - startTime <= timePerFlash + 0.1f)
            {
                if(imageToFlash == null)
                    break;

                imageToFlash.color = Color.Lerp(baseColor, flashColor, (Time.time - startTime)/timePerFlash);
                yield return null;
            }

            startTime = Time.time;
            while(Time.time - startTime <= timePerFlash + 0.1f)
            {
                if(imageToFlash == null)
                    break;

                imageToFlash.color = Color.Lerp(flashColor, baseColor, (Time.time - startTime)/timePerFlash);
                yield return null;
            }
        }
    }
}
