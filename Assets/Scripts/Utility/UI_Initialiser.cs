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

    public void SetText(string text, int index = 0)
    {
        texts[index].text = text;
    }

    public void SetImage(Sprite sprite, int index = 0)
    {
        images[index].sprite = sprite;
    }
}
