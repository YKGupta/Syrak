using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class TimerManager : MonoBehaviour
{
    [BoxGroup("Timer")]
    public TimeUtility timeAvailable;
    [BoxGroup("Timer")]
    public TMP_Text timerText;
    [BoxGroup("Timer")]
    public Image timerImage;
    [BoxGroup("Penalty")]
    public DoorManager doorManager;
    [BoxGroup("Penalty")]
    public int penaltyMultiplier = 2;
    [BoxGroup("Penalty")]
    [Range(0f, 600f)]
    public float penaltyDuration = 60f;
    [BoxGroup("Penalty")]
    public TMP_Text penaltyText;
    [BoxGroup("Penalty")]
    public GameObject penaltyGO;
    [BoxGroup("Penalty")]
    public float flashTime = 1f;
    [BoxGroup("Penalty")]
    public Color flashColor = new Color(1f, 1f, 1f, 1f);

    public event Action timeOver;

    private float timeElapsed;
    private float maxTime;

    private float penaltyMaxTime;
    private float penaltyTimeElapsed;
    private int penaltyFactor = 1;

    private TimeSpan timeSpan;

    private void Start()
    {
        maxTime = timeAvailable.seconds + timeAvailable.minutes * 60 + timeAvailable.hours * 3600;
        timeElapsed = 0f;
        timerImage.fillAmount = 1f;
        timeSpan = TimeSpan.FromSeconds(maxTime - timeElapsed);
        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");

        doorManager.penaltyIncurred += OnPenalty;
        penaltyMaxTime = penaltyDuration;
        penaltyText.text = "";
    }

    private void Update()
    {
        timeElapsed += penaltyFactor * Time.deltaTime;
        timeSpan = TimeSpan.FromSeconds(maxTime - timeElapsed);
        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");
        timerImage.fillAmount = 1 - timeElapsed / maxTime;

        if(timeElapsed >= maxTime)
        {
            if(timeOver != null)
                timeOver();

            enabled = false;
        }

        CheckPenaltyTime();
    }

    private void CheckPenaltyTime()
    {
        if(penaltyFactor == 1)
            return;
        
        penaltyTimeElapsed += Time.deltaTime;

        if(penaltyTimeElapsed >= penaltyMaxTime)
        {
            penaltyFactor = 1;
            penaltyTimeElapsed = 0f;
            penaltyText.text = "";
        }
    }

    public void OnPenalty()
    {
        penaltyFactor *= 2;
        penaltyTimeElapsed = 0f;
        penaltyText.text = $"<size=8>X</size> {penaltyFactor}";

        UI_Initialiser uI_Initialiser = penaltyGO.GetComponent<UI_Initialiser>();
        StartCoroutine(FlashImage(uI_Initialiser.GetImage(0), flashColor, 2, flashTime));
        StartCoroutine(FlashImage(uI_Initialiser.GetImage(1), flashColor, 2, flashTime));
        StartCoroutine(FlashImage(uI_Initialiser.GetImage(2), flashColor, 2, flashTime));
    }

    private IEnumerator FlashImage(Image image, Color flashColor, int freqency = 2, float flashTime = 1f)
    {
        Color baseColor = image.color;
        for(int i = 0; i < freqency; i++)
        {
            float startTime = Time.time;
            while(Time.time < startTime + flashTime/2)
            {
                if(image == null)
                    break;
                    
                image.color = Color.Lerp(baseColor, flashColor, (Time.time - startTime) / (flashTime/2));
                yield return null;
            }

            while(Time.time < startTime + flashTime)
            {
                if(image == null)
                    break;
                    
                image.color = Color.Lerp(flashColor, baseColor, (Time.time - startTime) / (flashTime/2));
                yield return null;
            }
        }
    }
}
