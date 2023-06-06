using UnityEngine;

namespace src.scripts.Deck
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public FgLibrary.CardsType cardType;
        public Mesh cardMesh;
    }
}
