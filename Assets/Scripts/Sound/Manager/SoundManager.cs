using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach(Sound s in sounds)
        {
            if(s.gameObject != null)
                s.source = s.gameObject.AddComponent<AudioSource>();
            else
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            if(s.source.playOnAwake)
                s.source.Play();
        }
    }

    public void Play(string clipName)
    {
        Sound s = Array.Find<Sound>(sounds, x => x.name.Equals(clipName));
        if(s == null)
        {
            Debug.LogError("Sound " + clipName + " not found :(");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipName)
    {
        Sound s = Array.Find<Sound>(sounds, x => x.name.Equals(clipName));
        if(s == null)
        {
            Debug.LogError("Sound " + clipName + " not found :(");
            return;
        }
        s.source.Stop();
    }
}