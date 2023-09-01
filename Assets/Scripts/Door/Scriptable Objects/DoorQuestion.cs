using UnityEngine;
using NaughtyAttributes;
using TMPro;

[System.Serializable]
public class DoorQuestion
{
    public string name;
    public QuestionType questionType;
    public QuestionPresentationMode mode;

    [HorizontalLine(color: EColor.Violet)]
    [ShowIf("mode", QuestionPresentationMode.Text)]
    [AllowNesting]
    public GameObject textCanvasGO;
    [ShowIf("mode", QuestionPresentationMode.Text)]
    [AllowNesting]
    public TMP_Text questionText;
    [ShowIf("mode", QuestionPresentationMode.Text)]
    [AllowNesting]
    public Question question;

    [ShowIf("mode", QuestionPresentationMode.Audio)]
    [AllowNesting]
    public AudioSource source;
    [ShowIf("mode", QuestionPresentationMode.Audio)]
    [AllowNesting]
    public AudioClip clip;
    [ShowIf("mode", QuestionPresentationMode.Audio)]
    [AllowNesting]
    public GameObject subtitlesCanvasGO;
    [ShowIf("mode", QuestionPresentationMode.Audio)]
    [AllowNesting]
    public TMP_Text subtitlesText;
    [ShowIf("mode", QuestionPresentationMode.Audio)]
    [AllowNesting]
    public Subtitles subtitles;

    [ShowIf("mode", QuestionPresentationMode.Visual)]
    [AllowNesting]
    public Animator animator;
    [ShowIf("mode", QuestionPresentationMode.Visual)]
    [AllowNesting]
    public string trigger;
}
