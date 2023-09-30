using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorFunctions : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public void PresentQuestion(Door door, DoorManager doorManager)
    {
        DoorQuestion temp = door.doorQuestion;
        doorManager.doorIsActive = true;
        switch(temp.mode)
        {
            case QuestionPresentationMode.MCQ:
            {
                GameObject questionGO = Instantiate(temp.questionPrefab, temp.panelGO.transform);
                questionGO.GetComponentInChildren<TMP_Text>().text = temp.question.question;
                
                foreach(Option op in temp.question.options)
                {
                    GameObject optionGO = Instantiate(temp.optionPrefab, temp.panelGO.transform);
                    optionGO.GetComponentInChildren<TMP_Text>().text = op.option;
                    op.associatedGO = optionGO;
                }

                temp.textCanvasGO.SetActive(true);
                break;
            }

            case QuestionPresentationMode.Key:
            {
                GameObject questionGO = Instantiate(temp.keyQuestionPrefab, temp.keyPanelGO.transform);
                questionGO.GetComponentInChildren<TMP_Text>().text = temp.keyQuestion.question;
                temp.keyQuestion.associatedGO = questionGO;

                temp.keyCanvasGO.SetActive(true);
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
            case QuestionPresentationMode.MCQ:
            {
                for(int i = 0; i < temp.panelGO.transform.childCount; i++)
                    Destroy(temp.panelGO.transform.GetChild(i).gameObject);

                temp.textCanvasGO.SetActive(false);
                break;
            }
        
            case QuestionPresentationMode.Key:
            {
                for(int i = 0; i < temp.keyPanelGO.transform.childCount; i++)
                    Destroy(temp.keyPanelGO.transform.GetChild(i).gameObject);

                temp.keyCanvasGO.SetActive(false);
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
        if(door.doorQuestion.mode != QuestionPresentationMode.MCQ && door.doorQuestion.mode != QuestionPresentationMode.Key)
            return false;
        switch(door.doorQuestion.mode)
        {
            case QuestionPresentationMode.MCQ:
            {
                foreach(Option op in door.doorQuestion.question.options)
                {
                    if(!Input.GetKeyDown(op.keyCode))
                        continue;

                    if(op.isCorrect)
                    {
                        Debug.Log("Correct Answer!!");
                        StartCoroutine(FlashColor(op.associatedGO.GetComponentInChildren<Image>(), op.correctColor));
                        door.enabled = false;
                        return true;
                    }
                    else
                    {
                        StartCoroutine(FlashColor(op.associatedGO.GetComponentInChildren<Image>(), op.incorrectColor));
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
                    StartCoroutine(FlashColor(keyQuestion.associatedGO.GetComponentInChildren<Image>(), keyQuestion.incorrectColor));
                }
                else
                {
                    Debug.Log("Found item!");
                    inventoryManager.RemoveItem(item, true, inventoryManager.inventoryGO.activeSelf);
                    StartCoroutine(FlashColor(keyQuestion.associatedGO.GetComponentInChildren<Image>(), keyQuestion.correctColor));
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
