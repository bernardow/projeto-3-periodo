using System;
using System.Collections.Generic;
using src.scripts.Managers;
using static src.scripts.FgLibrary;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Dropper : MonoBehaviour
    {
        private Hand _player;

        private void Start() => _player = GetComponent<Hand>();

        /// <summary>
        /// Atira um raio. Caso atinja a mesa retira a carta da mao do jogador e a coloca na mesa
        /// </summary>
        /// <param name="playerHand">Lista de cartas do jogador</param>
        /// <param name="selectedCards">Lista de cartas selecionados</param>
        /// <param name="mesaCards">Lista de cartas da mesa</param>
        public void DropCard(List<GameObject> playerHand, List<GameObject> selectedCards, List<string> mesaCards)
        {
            Ray ray = _player.playerCamera!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Mesa 1") && Input.GetMouseButtonDown(0) && selectedCards.Count is > 0 and < 2 && !mesaCards.Contains(selectedCards[0].name) && _player.playerManager.CanDrop(_player.mesaPlaces))
                {
                    GameObject card = selectedCards[0];
                    PlaceCard(card, _player.mesaPlaces);
                    playerHand.Remove(card);
                    selectedCards.Remove(card);
                    _player.playerManager.playerCardsNum--;
                    _player.playerManager.droppedCards++;
                    mesaCards.Add(card.name);
                    _player.turnManager.SkipTurn();
                }
            }
        }

        /// <summary>
        /// Coloca a carta na mesa
        /// </summary>
        /// <param name="card">Carta a ser baixada</param>
        /// <param name="places">Lugares das cartas</param>
        private void PlaceCard(GameObject card, List<Transform> places)
        {
            card.transform.position = places[0].position + Vector3.up * 0.1f;
            card.transform.rotation = Quaternion.identity;
            card.transform.rotation = Quaternion.Euler(90, 0, 0);

            Renderer render = GetChildComponent<Renderer>(card);
            render.material = _player.defaultMaterial;
            places.Remove(places[0]);
        }
    }
}
