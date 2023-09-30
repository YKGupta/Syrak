using UnityEngine;
using NaughtyAttributes;
using TMPro;

[System.Serializable]
public class DoorQuestion
{
    public string name;
    public QuestionPresentationMode mode;

    [HorizontalLine(color: EColor.Violet)]
    [ShowIf("mode", QuestionPresentationMode.MCQ)]
    [AllowNesting]
    public GameObject textCanvasGO;
    [ShowIf("mode", QuestionPresentationMode.MCQ)]
    [AllowNesting]
    public GameObject panelGO;
    [ShowIf("mode", QuestionPresentationMode.MCQ)]
    [AllowNesting]
    public GameObject questionPrefab;
    [ShowIf("mode", QuestionPresentationMode.MCQ)]
    [AllowNesting]
    public GameObject optionPrefab;
    [ShowIf("mode", QuestionPresentationMode.MCQ)]
    [AllowNesting]
    public Question question;

    [ShowIf("mode", QuestionPresentationMode.Key)]
    [AllowNesting]
    public GameObject keyCanvasGO;
    [ShowIf("mode", QuestionPresentationMode.Key)]
    [AllowNesting]
    public GameObject keyPanelGO;
    [ShowIf("mode", QuestionPresentationMode.Key)]
    [AllowNesting]
    public GameObject keyQuestionPrefab;
    [ShowIf("mode", QuestionPresentationMode.Key)]
    [AllowNesting]
    public KeyQuestion keyQuestion;

    [ShowIf("mode", QuestionPresentationMode.Subjective)]
    [AllowNesting]
    public GameObject subjectiveCanvasGO;
    [ShowIf("mode", QuestionPresentationMode.Subjective)]
    [AllowNesting]
    public GameObject subjectivePanelGO;
    [ShowIf("mode", QuestionPresentationMode.Subjective)]
    [AllowNesting]
    public GameObject subjectiveQuestionPrefab;
    [ShowIf("mode", QuestionPresentationMode.Subjective)]
    [AllowNesting]
    public GameObject subjectiveAnswerPrefab;
    [ShowIf("mode", QuestionPresentationMode.Subjective)]
    [AllowNesting]
    public SubjectiveQuestion subjectiveQuestion;

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
