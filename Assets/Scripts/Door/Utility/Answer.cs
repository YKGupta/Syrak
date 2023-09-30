using UnityEngine;

[System.Serializable]
public class Answer
{
    public string answer;

    [HideInInspector]
    public bool answered;
    
    [HideInInspector]
    public GameObject associatedGO;

    public void OnEndEdit(string text)
    {
        answered = true;
    }
}
