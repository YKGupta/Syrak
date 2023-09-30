using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class UI_Initialiser : MonoBehaviour
{
    [BoxGroup("UI")]
    public TMP_Text[] texts;
    [BoxGroup("UI")]
    public Image[] images;
    [BoxGroup("UI")]
    public TMP_InputField[] inputFields;

    public void SetText(string text, int index = 0)
    {
        texts[index].text = text;
    }

    public void SetImage(Sprite sprite, int index = 0)
    {
        images[index].sprite = sprite;
    }

    public void SetInputField(string text, int index = 0)
    {
        inputFields[index].text = text;
    }

    public TMP_Text GetText(int index = 0)
    {
        return texts[index];
    }

    public Image GetImage(int index = 0)
    {
        return images[index];
    }

    public TMP_InputField GetInputField(int index = 0)
    {
        return inputFields[index];
    }
}
