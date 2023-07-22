using UnityEngine;

namespace src.scripts.Managers
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;
    
        public float volume;
        public float pitch;

        public bool loop;

        public AudioSource source;
    }
}
