using UnityEngine;

namespace src.scripts.Deck
{
    /// <summary>
    /// Scriptable Object of the cards
    /// </summary>
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public Extensions.CardsType cardType; //Cards Type
        public int damage; //Damage of the card
    }
}
