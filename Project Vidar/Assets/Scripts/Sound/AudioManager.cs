using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    public String currentSong;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }

        Play("ambient");
    }

    public void Play(string sound)
    {
        currentSong = sound;
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            //Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            //Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (!s.source.isPlaying)
        {
            return;
        }

        // s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }
    public void stopAll()
    {
        foreach (var item in sounds)
        {
            if (!item.name.Equals("ambient") && !item.name.Equals("throw"))
            {
                Stop(item.name);
            }
            PlayerEntity.setIsPlayingGrassStep(false);
        }
    }

    public void setGeneralVolume(float value)
    {
        foreach (var item in sounds)
        {
            item.source.volume = item.volume * value;
        }
    }

}
