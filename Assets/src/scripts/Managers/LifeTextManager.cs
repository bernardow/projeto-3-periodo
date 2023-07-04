using System;
using TMPro;
using UnityEngine;

public class LifeTextManager : MonoBehaviour
{
    [SerializeField] private CardPlayer cardPlayer;
    private TextMeshProUGUI _textMeshProUGUI;

    private void Start() => _textMeshProUGUI = GetComponent<TextMeshProUGUI>();

    void Update() => _textMeshProUGUI.text = cardPlayer.life.ToString();
}
