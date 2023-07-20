using System.Collections.Generic;
using UnityEngine;

namespace src.scripts.Deck.Special_Cards
{
    public class DrawCard : MonoBehaviour, IObserverCard
    {
        private List<CardPlayer> _players = new List<CardPlayer>();
        private List<int> _lifes = new List<int>();

        
        /// <summary>
        /// Check the number of cards the player should draw
        /// </summary>
        /// <param name="myPlayer">CardPlayer that it`s calling it</param>
        private void CheckForDraw(CardPlayer myPlayer)
        {
            //Add all the players and lifes to a list todo use a dictionary?
            if (_players.Count == 0)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("CardPlayer");
                foreach (GameObject player in players)
                {
                    CardPlayer cardPlayer = player.GetComponent<CardPlayer>();
                    
                    _players.Add(cardPlayer);
                    _lifes.Add(cardPlayer.life);
                }
            }
            
            //Sort to find the one with less life
            _lifes.Sort();

            //Check if the player with less life it`s yours
            if (_lifes[0] == myPlayer.life)
            {
                Draw(3, myPlayer);
                return;
            }
            Draw(2, myPlayer);
        }

        /// <summary>
        /// Draw cards
        /// </summary>
        /// <param name="numOfCards">Number of cards that must draw</param>
        /// <param name="myPlayer">CardPlayer that it`s calling it</param>
        private void Draw(int numOfCards, CardPlayer myPlayer)
        {
            Hand.Hand myHand = myPlayer.GetComponentInChildren<Hand.Hand>();
            for(int i = 1; i <= numOfCards; i++)
                myHand.Puller.PullCard(myHand);
        }
    
        /// <summary>
        /// Used to trigger action
        /// </summary>
        /// <param name="player">Player that it`s calling it</param>
        public void OnNotify(CardPlayer player) => CheckForDraw(player);
    }
}
