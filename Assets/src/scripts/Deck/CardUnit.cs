using UnityEngine;

namespace src.scripts.Deck
{
    public class CardUnit : MonoBehaviour
    {
        [SerializeField] private FgLibrary.CardsType cardsType;

        private void OnEnable() => gameObject.name = cardsType.ToString();
    }
}
