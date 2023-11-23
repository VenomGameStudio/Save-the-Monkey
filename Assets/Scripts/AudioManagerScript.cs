using UnityEngine;
using System.Collections.Generic;

public class AudioManagerScript : MonoBehaviour
{
    public List<Sound> sounds;

    public bool _canPlayMusic = false;
    public bool CanPlayMusic
    {
        get => _canPlayMusic;
        set
        {
            _canPlayMusic = value;
            if (_canPlayMusic) PlayMusic("Theme");
            else StopMusic("Theme");
        }
    }
    public bool canPlayEffects = false;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.mute = s.mute;
            s.source.loop = s.loop;
        }

        GetSoundDataFromPlayerPrefs();
    }

    void GetSoundDataFromPlayerPrefs()
    {
        int musicValue = PlayerPrefs.GetInt(GenericKeys.Music, 1);
        CanPlayMusic = musicValue == 1;

        int effectsValue = PlayerPrefs.GetInt(GenericKeys.Effect, 1);
        canPlayEffects = effectsValue == 1;
    }

    public void PlayMusic(string name)
    {
        Sound s = sounds.Find((s) => s.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void StopMusic(string name)
    {
        Sound s = sounds.Find((s) => s.name == name);
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }

    public void PlayEffect(string name)
    {
        if (!canPlayEffects) return;

        Sound s = sounds.Find((s) => s.name == name);
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