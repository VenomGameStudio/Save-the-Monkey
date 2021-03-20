using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.mute = s.mute;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log(s.clip);
        if (s == null)
            return;
        s.source.Play();
    }

    public void SoundControl(bool isRunning)
    {
        if (isRunning)
            AudioListener.pause = true;
        else
            AudioListener.pause = false;
    }
}