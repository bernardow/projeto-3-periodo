using System.Collections.Generic;
using Photon.Pun;
using src.scripts.CardsSync;
using UnityEngine;

namespace src.scripts.Deck
{
    public class CardUnit : MonoBehaviourPunCallbacks
    {
        public Extensions.CardsType cardsType;
        public Card card;

        private ObservableCardsTransform _observableCards;
        
        private new void OnEnable()
        {
            // Makes a new instance of the ObservableCards and find the Players by their tag
            _observableCards = new ObservableCardsTransform();
            GameObject[] players = GameObject.FindGameObjectsWithTag("CardPlayer");
            List<CardPlayer> cardPlayers = new List<CardPlayer>();
            
            // Add all the CardPlayer component to the ObservableObject Observers list
            foreach (GameObject player in players)
                _observableCards.AddObserver(player.GetComponent<CardPlayer>());
            
            //Set name of the cards to it`s type
            gameObject.name = cardsType.ToString();
        }

        /// <summary>
        /// Notify Players that a card has changed it`s position and rotation
        /// </summary>
        public void NotifyPlayers()
        {
            Transform t = transform;
            Vector3 pos = t.position;
            Quaternion rot = t.rotation;
            _observableCards.NotifyObservers(pos.x, pos.y, pos.z, rot.x, rot.y, rot.z, rot.w, photonView.ViewID);
        }
    }
}
