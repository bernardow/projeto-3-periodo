using src.scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace src.scripts.Menu
{
    public class AudioOptions : MonoBehaviour
    {
        [SerializeField] private AudioManagerLists audioManagerList;
        private AudioManager _audioManager;
        private Slider _slider;

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _slider = GetComponent<Slider>();
        }

        private void Update() => PairAudio(audioManagerList);

        /// <summary>
        /// Pair the audios by category with the volume slider
        /// </summary>
        /// <param name="listName">Category</param>
        private void PairAudio(AudioManagerLists listName)
        {
            switch (listName)
            {
                case AudioManagerLists.Effects:
                    foreach (AudioSource sound in _audioManager.effects)
                        sound.volume = _slider.value;
                    
                    break;
                case AudioManagerLists.Musics:
                    foreach (AudioSource sound in _audioManager.musics)
                        sound.volume = _slider.value;
                    
                    break;
            }
        }

        /// <summary>
        /// Categories of the audios
        /// </summary>
        private enum AudioManagerLists
        {
            Effects,
            Musics
        }
    }
}
