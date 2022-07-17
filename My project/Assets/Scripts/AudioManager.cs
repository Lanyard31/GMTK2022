using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;


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

            //            s.source.volume = s.volume;
            //            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }



    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        /* If we have Pause

        if (Pause.GameIsPaused == true)
        {
            s.source.volume *= 0.5f;
        }
        */

        s.source.Play();
    }


    public void CheckIfPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (s.source.isPlaying == true)
        {
            return;
        }
        else
        {
            s.source.time = 17.2f;
            s.source.Play();
        }
    }

    public void Stop(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source.isPlaying == true)
        {
            s.source.Stop();
        }
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void PlayOneShot(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
                /* If we have Pause
        if (Pause.GameIsPaused == true)
        {
            s.source.volume *= 0.5f;
        }
                */

        s.source.PlayOneShot(s.clip, s.volume);
    }



}
