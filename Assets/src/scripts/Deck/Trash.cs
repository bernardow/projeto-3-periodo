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

        private void Start() => _trashPos = transform.position + Vector3.up * 0.1f;
    
        public void MoveToTrash(GameObject card, List<GameObject> handDeck, List<GameObject> selected, PlayerManager playerManager, Material defaulMaterial)
        {
            photonView.RPC("UpdateTrashCards", RpcTarget.All, card.GetComponent<PhotonView>().ViewID);
            handDeck.Remove(card);
            
            if(selected.Count > 0)
                selected.Remove(card);
            
            playerManager.playerCardsNum--;
            card.GetComponent<Transform>().SetParent(transform);
        }

        public void MoveMergedCardsToTrahs(List<GameObject> cards, List<GameObject> handDeck, List<GameObject> selected, PlayerManager playerManager, Material defaulMaterial)
        {
            List<Renderer> render = new List<Renderer>();
            foreach (var card in cards)
                render.Add(GetChildComponent<Renderer>(card));

            foreach (var ren in render)
                ren.material = defaulMaterial;
            
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
            Renderer render = GetChildComponent<Renderer>(card);
            render.material = defaultMaterial;
            card.transform.position = _trashPos;
            card.transform.rotation = Quaternion.Euler( transform.rotation.x - 90, transform.rotation.y - 90, 90);
            card.tag = "Trash";
            trashCards.Add(card);
            _trashPos += Vector3.up * 0.1f;
        }
    }
}
