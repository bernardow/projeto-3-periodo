using System.Collections.Generic;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Puller : Hand
    {
        private int _cardsPulled;
        
        public void Pull(List<GameObject> handDeck, Stack<GameObject> gameDeck, Vector3 spawnPos)
        {
            Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity) && Input.GetMouseButtonDown(0) && _cardsPulled < 1 && handDeck.Count < 4)
            {
                if(hitInfo.collider.gameObject.CompareTag("Deck"))
                    handDeck.Add(gameDeck.Pop());

            
                PlaceCard(handDeck, spawnPos);
                _cardsPulled++;
            }
        }

        private void PlaceCard(List<GameObject> handDeck, Vector3 spawnPos)
        {
            foreach (var card in handDeck)
            {
                card.transform.position = spawnPos;
                card.transform.LookAt(Camera.main!.transform.position);
                spawnPos += Vector3.left + Vector3.up * 0.3f;
            }
        }
    }
}
