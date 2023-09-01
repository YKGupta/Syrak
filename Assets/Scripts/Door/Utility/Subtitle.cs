[System.Serializable]
public class Subtitle
{
    public string text;
    public float duration;
}

[System.Serializable]
public class Subtitles
{
    public Subtitle[] subtitles;
}