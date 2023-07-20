using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Deck;
using UnityEngine;
using static src.scripts.Deck.Deck;

namespace src.scripts.Hand
{
    public class Puller : MonoBehaviour, IObservable
    {
        private Hand _player;

        //Assignments and pull two cards for initial game
        private void Start()
        {
            _player = GetComponent<Hand>();
            PullCard(_player);
            PullCard(_player);
            _player.PlayerManager.cardsPulled = 0;
        }

        /// <summary>
        /// Place the card in players hand
        /// </summary>
        public void PlaceCard() => _player.photonViewPlayer.RPC("PlaceCardsGlobal", RpcTarget.All);

        /// <summary>
        /// Avisa o player manager para ele verificar se o jogador pode comprar mais cartas
        /// </summary>
        /// <param name="handDeck">Lista das cartas do jogador</param>
        private void NotifyPlayerManager(List<GameObject> handDeck)
        {
            _player.PlayerManager.cardsPulled++;
            _player.PlayerManager.playerCardsNum = handDeck.Count;
        }

        /// <summary>
        /// Pulls a card and place it correctly
        /// </summary>
        /// <param name="hand">Target Hand</param>
        public void PullCard(Hand hand)
        {
            hand.player1Hand.Add(InGameDeck.Peek());
            PlaceCard();
            foreach (GameObject card in hand.player1Hand)
            {
                CardUnit cardUnit = card.GetComponent<CardUnit>();
                cardUnit.NotifyPlayers();
            }
            
            hand.photonViewPlayer.RPC("PopCard", RpcTarget.All);
            NotifyPlayerManager(_player.player1Hand);
        }

        /// <summary>
        /// Pulls the card
        /// </summary>
        /// <param name="hitTag">Check if it`s "Deck"</param>
        public void OnNotify(RaycastHit hitTag)
        {
            if (_player.PlayerManager.canPull && hitTag.collider.CompareTag("Deck"))
            {
                AudioManager.Instance.Play("DrawCardEffect");
                PullCard(_player);
            }
        }
    }
}
