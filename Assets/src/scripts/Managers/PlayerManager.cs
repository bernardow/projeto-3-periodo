using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace src.scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Button _skipTurnBtn;
        public int cardsPulled;
        public bool canPull;
        public int playerCardsNum;
        public int mergedCards;
        public int droppedCards;

        private void Awake() => _skipTurnBtn = GameObject.Find("Turn Btn").GetComponent<Button>();

        private void OnEnable()
        {
            cardsPulled = 0;
            mergedCards = 0;
            droppedCards = 0;
            CanSkipTurn(true);
        }

        private void OnDisable()
        {
            if(_skipTurnBtn!)
                CanSkipTurn(false);
        }

        private void Update()
        {
            CanPull();
        }

        private void CanSkipTurn(bool state) => _skipTurnBtn.interactable = state;
        
        
        private void CanPull()
        {
            if (cardsPulled < 1 && playerCardsNum < 4)
                canPull = true;
            else
                canPull = false;
        }

        public bool CanSelect() => !canPull;

        public bool CanMerge() => true;
    }
}
