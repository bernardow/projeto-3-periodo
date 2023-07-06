using UnityEngine;
using UnityEngine.UI;

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

    private void PairAudio(AudioManagerLists listName)
    {
        switch (listName)
        {
            case AudioManagerLists.Effects:
                foreach (AudioSource sound in _audioManager.effects)
                {
                    sound.volume = _slider.value;
                }
                break;
            case AudioManagerLists.Musics:
                foreach (AudioSource sound in _audioManager.musics)
                {
                    sound.volume = _slider.value;
                }
                break;
        }
    }

    private enum AudioManagerLists
    {
        Effects,
        Musics
    }
}
