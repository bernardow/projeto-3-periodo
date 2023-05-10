using System;
using UnityEngine;

namespace src.scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public int cardsPulled;
        public bool canPull;
        public int playerCardsNum;
        public int mergedCards;
        public int droppedCards;

        private void OnEnable()
        {
            cardsPulled = 0;
            mergedCards = 0;
            droppedCards = 0;
        }

        private void Update() => CanPull();
        
        private void CanPull()
        {
            if (cardsPulled < 1 && playerCardsNum < 4)
                canPull = true;
            else
                canPull = false;
        }

        public bool CanSelect() => !canPull;

        public bool CanMerge() => mergedCards < 1;

        public bool CanDrop() => droppedCards < 1;
    }
}
