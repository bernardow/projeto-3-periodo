using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Discard : MonoBehaviour, IObservable
    {
        private Hand _player;

        private void Start() => _player = GetComponent<Hand>();
        
        private void DiscardCard(GameObject card)
        {
            _player.trash.MoveToTrash(card, _player.player1Hand, _player._cardSelector.selectedCardsPlaye1, _player.playerManager, _player.defaultMaterial);
        }
        
        public void OnNotify(RaycastHit hitInfo)
        {
            if (hitInfo.collider.CompareTag("Trash") && _player._cardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                GameObject card = _player._cardSelector.selectedCardsPlaye1[0];
                IObserverCard observer = card.GetComponent<IObserverCard>();
                
                DiscardCard(card);
                                
                if(observer != null)
                    observer.OnNotify(_player.cardPlayer);
                else
                    _player.turnManager.GetComponent<PhotonView>().RPC("SkipTurn", RpcTarget.All);
            }
            else if (hitInfo.collider.CompareTag("Trash") && _player._cardSelector.selectedCardsPlaye1.Count > 1)
            {
                List<GameObject> selectedCardsArray = new List<GameObject>();
                foreach (var card in _player._cardSelector.selectedCardsPlaye1)
                    selectedCardsArray.Add(card);

                FgLibrary.CardsType mergedColor;
                _player.merge.CheckMergePossibilities(_player._cardSelector.selectedCardsPlaye1[0], _player._cardSelector.selectedCardsPlaye1[1], out mergedColor);
                if (mergedColor != FgLibrary.CardsType.Joker)
                {
                    _player.trash.MoveMergedCardsToTrahs(selectedCardsArray, _player.player1Hand, _player._cardSelector.selectedCardsPlaye1, _player.playerManager, _player.defaultMaterial);
                    _player.merge.GetMergedColor(mergedColor, _player.player1Hand, _player);
                    _player.playerManager.mergedCards++;
                }
            }
        }
    }
}
