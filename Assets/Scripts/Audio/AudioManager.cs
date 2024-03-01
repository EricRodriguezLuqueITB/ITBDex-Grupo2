using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] music;

    public float volume;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        SetAudioParameters(sounds, "Sound");
        SetAudioParameters(music, "Music");

    }
    private void SetAudioParameters(Sound[] array, string type)
    {
        foreach (Sound sound in array)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = PlayerPrefs.GetFloat(type);
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("Main", music);
    }
    public void ChoosePlay(string name, int id)
    {
        switch (id)
        {
            case 0:
                Play(name, sounds);
                break;

            case 1:
                Play(name, music);
                break;

            default: break;
        }

    }
    public void Play(string name, Sound[] array)
    {
        Sound sound = Array.Find(array, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound: " + sound.name + " not found");
            return;
        }

        //if (PauseMenu.GameIsPaused)
        //{
        //    sound.source.pitch *= .5f;
        //}

        sound.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(music, item => item.name == sound);

        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }

        s.source.Stop();
    }

    public void ChangeVolumen(float volum, int id)
    {
        switch (id)
        {
            case 0:
                foreach (Sound sound in sounds)
                {
                    sound.source.volume = sound.volume * volum;
                }
                break;
            case 1:
                foreach (Sound sound in music)
                {
                    sound.source.volume = sound.volume * volum;
                }
                break;
            default:
                break;
        }

    }

    public void SaveVolume(string audio, float level)
    {
        PlayerPrefs.SetFloat(audio, level);
    }
}