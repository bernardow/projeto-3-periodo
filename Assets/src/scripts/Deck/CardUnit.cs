using System;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Deck
{
    public class CardUnit : MonoBehaviourPunCallbacks
    {
        public FgLibrary.CardsType cardsType;
        public Card card;

        private ObservableCardsTransform _observableCards;
        
        private void OnEnable()
        {
            _observableCards = new ObservableCardsTransform();
            foreach (CardPlayer cardPlayer in FindObjectsOfType<CardPlayer>())
                _observableCards.AddObserver(cardPlayer);
            gameObject.name = cardsType.ToString();
        }

        public void NotifyPlayers()
        {
            _observableCards.NotifyObservers(transform.position.x, transform.position.y, transform.position.z, transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w, photonView.ViewID);
        }
    }
}
