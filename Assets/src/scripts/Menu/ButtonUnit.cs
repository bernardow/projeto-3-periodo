using src.scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace src.scripts.Menu
{
    /// <summary>
    /// Used to play hover audio
    /// </summary>
    public class ButtonUnit : MonoBehaviour, IPointerEnterHandler
    {
        private AudioManager _audioManager;

        private void Start() => _audioManager = FindObjectOfType<AudioManager>();

        public void OnPointerEnter(PointerEventData eventData) => _audioManager.Play("HoverEffect");
    }
}
