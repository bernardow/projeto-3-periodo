using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Deck
{
    public class Trash : MonoBehaviourPunCallbacks
    {
        private Vector3 _trashPos;  //TrashCards position
        public List<GameObject> trashCards = new List<GameObject>();    //Trash Cards List

        //Set the trahsPos to a little upwadrs to make move to next card
        private void Start() => _trashPos = transform.position + Vector3.up * 0.1f;

        /// <summary>
        /// Move a card to Trash
        /// </summary>
        /// <param name="card">Card to move</param>
        /// <param name="hand">Hand that the card is being removed from</param>
        public void MoveToTrash(GameObject card, Hand.Hand hand)
        {
            photonView.RPC("UpdateTrashCards", RpcTarget.All, card.GetComponent<PhotonView>().ViewID);
            hand.player1Hand.Remove(card);
         
            //Check if player is selecting more than 1 card
            if(hand.CardSelector.selectedCardsPlaye1.Count > 0)
                hand.CardSelector.selectedCardsPlaye1.Remove(card);
            
            //Change the number of cards the player have in hands
            hand.PlayerManager.playerCardsNum--;
            
            //Set parent to Trash
            card.GetComponent<Transform>().SetParent(transform);
        }

        /// <summary>
        /// Discard two cards of the hand in return for a Special Color
        /// </summary>
        /// <param name="cards">Hand Deck</param>
        /// <param name="hand">Hand that the card it`s being removed from</param>
        public void MoveMergedCardsToTrash(List<GameObject> cards, Hand.Hand hand)
        {
            foreach (var card in cards)
            {
                photonView.RPC("UpdateTrashCards", RpcTarget.All, card.GetComponent<PhotonView>().ViewID);
                hand.player1Hand.Remove(card);
                hand.CardSelector.selectedCardsPlaye1.Remove(card);
            }
            hand.PlayerManager.playerCardsNum--;
        }

        /// <summary>
        /// Sync information to all the players in the room about the new card thats it`s going to trash
        /// </summary>
        /// <param name="id">ID of the Card thats it`s going to Trash</param>
        [PunRPC]
        private void UpdateTrashCards(int id)
        {
            //Get the GameObjects and disable the outline
            GameObject card = PhotonView.Find(id).gameObject;
            GameObject outline = card.transform.GetChild(0).GetChild(0).gameObject;
            outline.SetActive(false);

            //Cache Transform
            Transform cardTransform = card.transform;
            
            //Set position and rotation
            cardTransform.SetPositionAndRotation(_trashPos,Quaternion.Euler( transform.rotation.x - 270, transform.rotation.y - 90, 90));
            
            //Set Tag and Add to trash list
            card.tag = "Trash";
            trashCards.Add(card);
            
            //Move TrashPos to make room for new cards
            _trashPos += Vector3.up * 0.1f;

            //Notify player that the card transform has changed
            foreach (GameObject trashCard in trashCards)
            {
                CardUnit cardUnit = trashCard.GetComponent<CardUnit>();
                cardUnit.NotifyPlayers();
            }
        }
    }
}
