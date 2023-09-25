using UnityEngine;

[System.Serializable]
public class TimeUtility
{
    [Range(0, 100)]
    public int hours;
    [Range(0, 59)]
    public int minutes;
    [Range(0, 59)]
    public int seconds;
}
