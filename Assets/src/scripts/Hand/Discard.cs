using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Discard : MonoBehaviour
    {
        private Hand _player;

        private void Start() => _player = GetComponent<Hand>();

        /// <summary>
        /// Atira um raio. Se o raio colidir com o lixo, ele pega a carta selecionada e joga fora. Tirando da lista das cartas do jogador
        /// </summary>
        /// <param name="handDeck">Lista de cartas da mao do jogador</param>
        /// <param name="selectedCards">Lista de cartas selecionadas</param>
        public void DiscardCard(List<GameObject> handDeck, List<GameObject> selectedCards)
        {
            Ray ray = _player.playerCamera!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider.CompareTag("Trash") && selectedCards.Count is > 0 and < 2)
                {
                    _player.trash.MoveToTrash(selectedCards[0], handDeck, selectedCards, _player.playerManager, _player.defaultMaterial);
                    _player.turnManager.GetComponent<PhotonView>().RPC("SkipTurn", RpcTarget.All);
                }
                else if (hit.collider.CompareTag("Trash") && selectedCards.Count > 1)
                {
                    List<GameObject> selectedCardsArray = new List<GameObject>();
                    foreach (var card in selectedCards)
                        selectedCardsArray.Add(card);

                    FgLibrary.CardsType mergedColor;
                    _player.merge.CheckMergePossibilities(selectedCards[0], selectedCards[1], out mergedColor);
                    if (mergedColor != FgLibrary.CardsType.Joker && _player.playerManager.CanMerge())
                    {
                        _player.trash.MoveMergedCardsToTrahs(selectedCardsArray, handDeck, selectedCards, _player.playerManager, _player.defaultMaterial);
                        _player.merge.GetMergedColor(mergedColor, handDeck, _player);
                        _player.playerManager.mergedCards++;
                    }
                }
            }
        }
    }
}
