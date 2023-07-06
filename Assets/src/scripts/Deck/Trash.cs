using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using static src.scripts.FgLibrary;

namespace src.scripts.Deck
{
    public class Trash : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Material defaultMaterial;
        private Vector3 _trashPos;
        public List<GameObject> trashCards = new List<GameObject>();

        public PhotonView trashPhotonView;

        private void Start()
        {
            trashPhotonView = photonView;
            
            _trashPos = transform.position + Vector3.up * 0.1f;
        }

        public void MoveToTrash(GameObject card, List<GameObject> handDeck, List<GameObject> selected, PlayerManager playerManager)
        {
            photonView.RPC("UpdateTrashCards", RpcTarget.All, card.GetComponent<PhotonView>().ViewID);
            handDeck.Remove(card);
            
            if(selected.Count > 0)
                selected.Remove(card);
            
            playerManager.playerCardsNum--;
            card.GetComponent<Transform>().SetParent(transform);
        }

        public void MoveMergedCardsToTrahs(List<GameObject> cards, List<GameObject> handDeck, List<GameObject> selected, PlayerManager playerManager)
        {
            foreach (var card in cards)
            {
                photonView.RPC("UpdateTrashCards", RpcTarget.All, card.GetComponent<PhotonView>().ViewID);
                handDeck.Remove(card);
                selected.Remove(card);
            }
            playerManager.playerCardsNum--;
        }

        [PunRPC]
        private void UpdateTrashCards(int id)
        {
            GameObject card = PhotonView.Find(id).gameObject;
            GameObject outline = card.transform.GetChild(0).GetChild(0).gameObject;
            outline.SetActive(false);
            
            card.transform.position = _trashPos;
            card.transform.rotation = Quaternion.Euler( transform.rotation.x - 270, transform.rotation.y - 90, 90);
            card.tag = "Trash";
            trashCards.Add(card);
            _trashPos += Vector3.up * 0.1f;

            foreach (GameObject trashCard in trashCards)
            {
                CardUnit cardUnit = trashCard.GetComponent<CardUnit>();
                cardUnit.NotifyPlayers();
            }
        }
    }
}
