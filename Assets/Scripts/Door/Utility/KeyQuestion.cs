using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class KeyQuestion
{
    [ResizableTextArea]
    public string question;
    public int itemId;
    public KeyCode keyCode;
    public Color correctColor = new Color(0.09f, 1f, 0f, 0.5f);
    public Color incorrectColor = new Color(1f, 0f, 0f, 0.5f);
    
    [HideInInspector]
    public GameObject associatedGO;
}
