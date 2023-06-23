using UnityEngine;

namespace src.scripts.Deck
{
    public class CardUnit : MonoBehaviour
    {
        public FgLibrary.CardsType cardsType;

        private void OnEnable() => gameObject.name = cardsType.ToString();
    }
}
