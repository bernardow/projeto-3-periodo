using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public List<AudioSource> effects = new List<AudioSource>();
    public List<AudioSource> musics = new List<AudioSource>();

    public static AudioManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            
            string[] soundName = Regex.Split(sound.name, @"(?<!^)(?=[A-Z])");
            if(soundName[^1] == "Effect")
                effects.Add(sound.source);
            else musics.Add(sound.source);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " +  name + " not found");
            return;
        }
        s.source.Play(); 
    }
    
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " +  name + " not found");
            return;
        }
        s.source.Stop(); 
    }
}
