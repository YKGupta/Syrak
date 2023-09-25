using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TimeUtility timeAvailable;
    public TMP_Text timerText;
    public Image timerImage;

    public event Action timeOver;

    private float startTime;
    private float maxTime;

    private TimeSpan timeSpan;

    private void Start()
    {
        maxTime = timeAvailable.seconds + timeAvailable.minutes * 60 + timeAvailable.hours * 3600;
        startTime = Time.time;
        timerImage.fillAmount = 1f;
        timeSpan = TimeSpan.FromSeconds(maxTime - (Time.time - startTime));
        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");
    }

    private void Update()
    {
        timeSpan = TimeSpan.FromSeconds(maxTime - (Time.time - startTime));
        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");
        timerImage.fillAmount = 1 - (Time.time - startTime) / maxTime;

        if(Time.time - startTime >= maxTime)
        {
            if(timeOver != null)
                timeOver();

            enabled = false;
        }
    }
}
