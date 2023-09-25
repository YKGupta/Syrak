using UnityEngine;

[System.Serializable]
public class Option
{
    public string option;
    public KeyCode keyCode;
    public bool isCorrect = false;
    public Color correctColor = new Color(0.09f, 1f, 0f, 0.5f);
    public Color incorrectColor = new Color(1f, 0f, 0f, 0.5f);

    [HideInInspector]
    public GameObject associatedGO;
}