using System;
using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Deck;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.Deck.Deck;

namespace src.scripts.Hand
{
    public class Puller : MonoBehaviour, IObservable
    {
        private Hand _player;

        private void Start()
        {
            _player = GetComponent<Hand>();
            PullCard(_player);
            PullCard(_player);
            _player.playerManager.cardsPulled = 0;
        }

        /// <summary>
        /// Coloca a carta na mao do jogador
        /// </summary>
        /// <param name="handDeck">Lista de cartas do jogador</param>
        /// <param name="spawnPos">Local de spawn das cartas</param>
        public void PlaceCard(List<GameObject> handDeck, Vector3 spawnPos)
        {
            _player.photonViewPlayer.RPC("PlaceCardsGlobal", RpcTarget.All);
        }

        /// <summary>
        /// Avisa o player manager para ele verificar se o jogador pode comprar mais cartas
        /// </summary>
        /// <param name="handDeck">Lista das cartas do jogador</param>
        private void NotifyPlayerManager(List<GameObject> handDeck)
        {
            _player.playerManager.cardsPulled++;
            _player.playerManager.playerCardsNum = handDeck.Count;
        }

        public void PullCard(Hand hand)
        {
            hand.player1Hand.Add(InGameDeck.Peek());
            PlaceCard(hand.player1Hand, hand.cardsPos.position);
            foreach (GameObject card in hand.player1Hand)
            {
                CardUnit cardUnit = card.GetComponent<CardUnit>();
                cardUnit.NotifyPlayers();
            }
            
            hand.photonViewPlayer.RPC("PopCard", RpcTarget.All);
            NotifyPlayerManager(_player.player1Hand);
        }

        public void OnNotify(RaycastHit hitTag)
        {
            if (_player.playerManager.canPull && hitTag.collider.CompareTag("Deck"))
            {
                AudioManager.Instance.Play("DrawCardEffect");
                PullCard(_player);
            }
        }
    }
}
