using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    public String currentSong;

    Scene currentScene;
    string sceneName;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
        if (sceneName.Equals("Tutorial"))
        {
            Play("ambient");
        } else if (sceneName.Equals("Level 1"))
        {
            Play("ambient2");
        }
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
            if (!item.name.Equals("ambient") && !item.name.Equals("throw") && !item.name.Equals("ambient2"))
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
