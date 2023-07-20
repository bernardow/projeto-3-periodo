using System.Collections.Generic;
using src.scripts.Deck;
using UnityEngine;
using static src.scripts.Extensions;

namespace src.scripts.Hand
{
    public class Merge : MonoBehaviour
    {
        [Header("Cards")]
        [SerializeField] private List<Card> cards;

        [Header("Merge Datas")]
        [SerializeField] private List<MergeData> mergeDatas;

        //Special cards Stacks
        public static Stack<GameObject> GreenCards = new Stack<GameObject>();
        public static Stack<GameObject> PurpleCards = new Stack<GameObject>();
        public static Stack<GameObject> OrangeCards = new Stack<GameObject>();
        public static Stack<GameObject> CianCards = new Stack<GameObject>();
        public static Stack<GameObject> PinkCards = new Stack<GameObject>();
        public static Stack<GameObject> BrownCards = new Stack<GameObject>();

        /// <summary>
        /// Compare 2 colors and look for possible merges
        /// </summary>
        /// <param name="color1">Color 1</param>
        /// <param name="color2">Color 2</param>
        /// <param name="mergedColor">Type returned</param>
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
                    if ((data.mergeColor1 == color1Type || data.mergeColor1 == color2Type) && (data.mergeColor2 == color2Type || data.mergeColor2 == color1Type))
                        mergedColor = data.color;
                }
            }
        }

        /// <summary>
        /// Returns the right color after check. Pops it from the right stack and deliver to the player
        /// </summary>
        /// <param name="cardType">Card Type</param>
        /// <param name="player">Target Player</param>
        public void GetMergedColor(CardsType cardType, Hand player)
        {
            switch (cardType)
            {
                case CardsType.Brown:
                    ColorAssignments(player, BrownCards);
                    break;
                case CardsType.Orange:
                    ColorAssignments(player, OrangeCards);
                    break;
                case CardsType.Pink:
                    ColorAssignments(player, PinkCards);
                    break;
                case CardsType.Green:
                    ColorAssignments(player, GreenCards);
                    break;
                case CardsType.Cian:
                    ColorAssignments(player, CianCards);
                    break;
                case CardsType.Purple:
                    ColorAssignments(player, PurpleCards);
                    break;
            }
        }

        /// <summary>
        /// Give the player the right color after merge
        /// </summary>
        /// <param name="player">Target Player</param>
        /// <param name="colorStack"></param>
        private void ColorAssignments(Hand player, Stack<GameObject> colorStack)
        {
            player.player1Hand.Add(colorStack.Pop());
            NotifyHand(player);
        }

        /// <summary>
        /// Rearrange player cards
        /// </summary>
        /// <param name="player">Target player</param>
        private void NotifyHand(Hand player) => player.RearrangeCards();
    }
}
