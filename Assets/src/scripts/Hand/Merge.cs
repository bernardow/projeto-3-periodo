using System.Collections.Generic;
using src.scripts.Deck;
using UnityEngine;
using static src.scripts.FgLibrary;

namespace src.scripts.Hand
{
    public class Merge : MonoBehaviour
    {
        [Header("Cards")]
        [SerializeField] private List<Card> cards;

        [Header("Merge Datas")]
        [SerializeField] private List<MergeData> mergeDatas;

        public Stack<GameObject> greenCards = new Stack<GameObject>();
        public Stack<GameObject> purpleCards = new Stack<GameObject>();
        public Stack<GameObject> orangeCards = new Stack<GameObject>();
        public Stack<GameObject> cianCards = new Stack<GameObject>();
        public Stack<GameObject> pinkCards = new Stack<GameObject>();
        public Stack<GameObject> brownCards = new Stack<GameObject>();


        public void CheckMergePossibilities(GameObject color1, GameObject color2, out CardsType mergedColor)
        {
            mergedColor = CardsType.Joker;
        
            CardsType color1Type = CardsType.Joker;
            CardsType color2Type = CardsType.Joker;
        
            foreach (var card in cards)
            {
                if (color1.name == card.name)
                    color1Type = card.cardType;
                else if (color2.name == card.name)
                    color2Type = card.cardType;
            }

            if (color1Type != color2Type)
            {
                foreach (var data in mergeDatas)
                {
                    if ((data.mergeColor1 == color1Type || data.mergeColor1 == color2Type) && (data.mergeColor2 == color2Type || data.mergeColor2 == color1Type)){}
                        mergedColor = data.color;
                }
            }
        }

        public void GetMergedColor(CardsType cardType, List<GameObject> hand, Hand player)
        {
            switch (cardType)
            {
                case CardsType.Brown:
                    ColorAssignments(player, hand, brownCards);
                    break;
                case CardsType.Orange:
                    ColorAssignments(player, hand, orangeCards);
                    break;
                case CardsType.Pink:
                    ColorAssignments(player, hand, pinkCards);
                    break;
                case CardsType.Green:
                    ColorAssignments(player, hand, greenCards);
                    break;
                case CardsType.Cian:
                    ColorAssignments(player, hand, cianCards);
                    break;
                case CardsType.Purple:
                    ColorAssignments(player, hand, purpleCards);
                    break;
            }
        }

        private void ColorAssignments(Hand player, List<GameObject> hand, Stack<GameObject> colorStack)
        {
            hand.Add(colorStack.Pop());
            GameObject card = hand[hand.Count - 1];
            NotifyHand(player);
        }

        private void NotifyHand(Hand player) => player.RearrangeCards();
    }
}
