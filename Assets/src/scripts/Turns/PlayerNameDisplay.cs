using UnityEngine;

namespace src.scripts.Turns
{
    /// <summary>
    /// Used to show player name each turn
    /// </summary>
    public class PlayerNameDisplay : MonoBehaviour
    {
        public void Deactivate() => gameObject.SetActive(false);
    }
}
