using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class SubjectiveQuestion
{
    [ResizableTextArea]
    public string question;
    public Color correctColor = new Color(0.09f, 1f, 0f, 0.5f);
    public Color incorrectColor = new Color(1f, 0f, 0f, 0.5f);
    public Answer answer;
    
    [HideInInspector]
    public GameObject associatedGO;
}
