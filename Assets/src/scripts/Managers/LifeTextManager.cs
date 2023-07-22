using TMPro;
using UnityEngine;

namespace src.scripts.Managers
{
    /// <summary>
    /// Updates Life text from the players
    /// </summary>
    public class LifeTextManager : MonoBehaviour
    {
        [SerializeField] private CardPlayer cardPlayer;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Start() => _textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        void Update() => _textMeshProUGUI.text = cardPlayer.life.ToString();
    }
}
