using System;
using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.Deck.Deck;

namespace src.scripts.Hand
{
    public class Puller : MonoBehaviour
    {
        private Hand _player;

        private void Start() => _player = GetComponent<Hand>();

        /// <summary>
        /// Atira um raio, se atingir o deck, ele puxa a carta de cima, coloca a carta na mao do jogador e notifica o Player Manager
        /// </summary>
        /// <param name="handDeck">Lista de cartas da mao do jogador</param>
        /// <param name="gameDeck">Deck do jogo</param>
        /// <param name="spawnPos">Qual vetor vai ser usado para colocar a carta</param>
        public void Pull(List<GameObject> handDeck, Stack<GameObject> gameDeck, Vector3 spawnPos)
        {
            Ray ray = _player.playerCamera!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity) && Input.GetMouseButtonDown(0) && _player.playerManager.canPull)
            {
                if (hitInfo.collider.gameObject.CompareTag("Deck"))
                {
                    handDeck.Add(gameDeck.Peek());
                    _player.photonViewPlayer.RPC("PopCard", RpcTarget.All);
                    PlaceCard(handDeck, spawnPos);
                    NotifyPlayerManager(handDeck);
                }
            }
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
    }
}
