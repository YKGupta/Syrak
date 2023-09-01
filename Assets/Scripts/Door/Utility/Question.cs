using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class Question
{
    [ResizableTextArea]
    public string question;
    public Option[] options;
}