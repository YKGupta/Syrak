using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public GameObject gameObject;
    public AudioSource source;
    public AudioClip clip;
    public float volume;
    public bool loop;
    public bool playOnAwake;
}