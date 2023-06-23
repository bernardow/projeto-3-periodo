using UnityEngine;

namespace src.scripts.Deck
{
    public class CardUnit : MonoBehaviour
    {
        public FgLibrary.CardsType cardsType;
        public Card card;

        private void OnEnable() => gameObject.name = cardsType.ToString();
    }
}
