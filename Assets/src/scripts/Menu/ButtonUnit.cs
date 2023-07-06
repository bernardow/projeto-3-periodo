using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUnit : MonoBehaviour, IPointerEnterHandler
{
    private AudioManager _audioManager;

    private void Start() => _audioManager = FindObjectOfType<AudioManager>();

    public void OnPointerEnter(PointerEventData eventData) => _audioManager.Play("HoverEffect");
}
