using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Deck.Special_Cards;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Discard : MonoBehaviour, IObservable
    {
        private Hand _player; //Player reference

        private void Start() => _player = GetComponent<Hand>(); //Assignments
        
        /// <summary>
        /// Send a card to trash
        /// </summary>
        /// <param name="card">Card that`s going to trash</param>
        private void DiscardCard(GameObject card) => _player.trash.MoveToTrash(card, _player);

        /// <summary>
        /// Look for a Special Card (Like force discard or Draw Cards) and execute it`s command 
        /// </summary>
        /// <param name="card">Card that`s being checked</param>
        private void CheckForSpecialCards(GameObject card)
        {
            IObserverCard observer = card.GetComponent<IObserverCard>();

            if(observer != null)
                observer.OnNotify(_player.CardPlayer);
            else
                _player.turnManager.GetComponent<PhotonView>().RPC("SkipTurn", RpcTarget.All);
        }

        /// <summary>
        /// Pickup the two selected cards and check if there`s a merge possibility. If it does send the two cards to trash and get the merged color
        /// </summary>
        private void DiscardForMerge()
        {
            List<GameObject> selectedCardsArray = new List<GameObject>();
            foreach (var card in _player.CardSelector.selectedCardsPlaye1)
                selectedCardsArray.Add(card);

            Extensions.CardsType mergedColor;
            _player.merge.CheckMergePossibilities(_player.CardSelector.selectedCardsPlaye1[0], _player.CardSelector.selectedCardsPlaye1[1], out mergedColor);
            if (mergedColor != Extensions.CardsType.Joker)
            {
                _player.trash.MoveMergedCardsToTrash(selectedCardsArray, _player);
                _player.merge.GetMergedColor(mergedColor, _player);
                _player.PlayerManager.mergedCards++;
            }   
        }
        
        /// <summary>
        /// Does the verification of the number of selected cards and check what must be done
        /// </summary>
        /// <param name="hitInfo">Check if it`s trash tag</param>
        public void OnNotify(RaycastHit hitInfo)
        {
            if (hitInfo.collider.CompareTag("Trash") && _player.CardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                GameObject card = _player.CardSelector.selectedCardsPlaye1[0];
                
                CheckForSpecialCards(card);
                DiscardCard(card);

                AudioManager.Instance.Play("DrawCardEffect");
                return;
            }
            
            if (hitInfo.collider.CompareTag("Trash") && _player.CardSelector.selectedCardsPlaye1.Count > 1)
            {
                DiscardForMerge();
                AudioManager.Instance.Play("DrawCardEffect");
                return;
            }
            
            AudioManager.Instance.Play("DeniedBtnEffect");
        }
    }
}
