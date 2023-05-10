using System.Collections.Generic;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.FgLibrary;

namespace src.scripts.Deck
{
    public class Trash : MonoBehaviour
    {
        private Vector3 _trashPos;
        public List<GameObject> trashCards = new List<GameObject>();

        private void Start() => _trashPos = transform.position + Vector3.up * 0.1f;
    
        public void MoveToTrash(GameObject card, List<GameObject> handDeck, List<GameObject> selected, PlayerManager playerManager, Material defaulMaterial)
        {
            Renderer render = GetChildComponent<Renderer>(card);
            render.material = defaulMaterial;
            card.transform.position = _trashPos;
            card.transform.rotation = Quaternion.Euler( transform.rotation.x - 90, transform.rotation.y - 90, 90);
            card.tag = "Trash";
            handDeck.Remove(card);
            selected.Remove(card);
            playerManager.playerCardsNum--;
            trashCards.Add(card);
            card.GetComponent<Transform>().SetParent(transform);
            _trashPos += Vector3.up * 0.1f;
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
                card.transform.position = _trashPos;
                card.transform.rotation = Quaternion.Euler( transform.rotation.x - 90, transform.rotation.y - 90, 90);
                card.tag = "Trash";
                handDeck.Remove(card);
                selected.Remove(card);
                
                trashCards.Add(card);
                card.GetComponent<Transform>().SetParent(transform);
                _trashPos += Vector3.up * 0.1f;
            }
            playerManager.playerCardsNum--;
        }
    }
}
