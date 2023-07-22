using UnityEngine;
using UnityEngine.UI;

namespace src.scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Button _skipTurnBtn;
        public int cardsPulled;
        public int playerCardsNum;
        public bool CanPull { get; private set; }

        //Gets button
        private void Awake() => _skipTurnBtn = GameObject.Find("Turn Btn").GetComponent<Button>();

        //Reset the number of cards that the player pulled this round
        private void OnEnable()
        {
            cardsPulled = 0;
            CanSkipTurn(true);
        }

        //Disables skip turn button when the round is over
        private void OnDisable()
        {
            if(_skipTurnBtn!)
                CanSkipTurn(false);
        }

        //Checks if can pull
        private void Update() => CanPullCard();
        
        //Change the interaction with the skip turn button
        private void CanSkipTurn(bool state) => _skipTurnBtn.interactable = state;
        
        /// <summary>
        /// Checks if Player can pull card
        /// </summary>
        private void CanPullCard()
        {
            if (cardsPulled < 1 && playerCardsNum < 4)
                CanPull = true;
            else
                CanPull = false;
        }

        /// <summary>
        /// Checks if player can select a card
        /// </summary>
        /// <returns></returns>
        public bool CanSelect() => !CanPull;
    }
}
