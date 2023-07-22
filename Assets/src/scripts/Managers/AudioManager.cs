using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace src.scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public List<AudioSource> effects = new List<AudioSource>();
        public List<AudioSource> musics = new List<AudioSource>();

        public static AudioManager Instance;
    
        //Sets singleton
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

            //Assignments
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

        #region Public Methods

        /// <summary>
        /// Search an audio by name and play it
        /// </summary>
        /// <param name="audioName">Name of the audio</param>
        public void Play(string audioName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == audioName);
            if (s == null)
            {
                Debug.LogWarning("Sound: " +  audioName + " not found");
                return;
            }
            s.source.Play(); 
        }
    
        /// <summary>
        /// Search an audio by name and stops it
        /// </summary>
        /// <param name="audioName"></param>
        public void Stop(string audioName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == audioName);
            if (s == null)
            {
                Debug.LogWarning("Sound: " +  audioName + " not found");
                return;
            }
            s.source.Stop(); 
        }
        #endregion
    }
}
