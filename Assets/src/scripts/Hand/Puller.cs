using System.Collections.Generic;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.FgLibrary;

namespace src.scripts.Hand
{
    public class Puller : Hand
    {
        public void Pull(List<GameObject> handDeck, Stack<GameObject> gameDeck, Vector3 spawnPos, PlayerManager player)
        {
            Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity) && Input.GetMouseButtonDown(0) && player.canPull)
            {
                if (hitInfo.collider.gameObject.CompareTag("Deck"))
                {
                    handDeck.Add(gameDeck.Pop());
                    PlaceCard(handDeck, spawnPos);
                    NotifyPlayerManager(handDeck, player);
                }
            }
        }

        public void PlaceCard(List<GameObject> handDeck, Vector3 spawnPos)
        {
            foreach (var card in handDeck)
            {
                card.transform.position = spawnPos;
                card.transform.LookAt(Camera.main!.transform.position);
                card.tag = "MyCards";
                card.GetComponent<Transform>().SetParent(handTransform);
                spawnPos += Vector3.left + Vector3.up * 0.3f;
            }
        }

        private void NotifyPlayerManager(List<GameObject> handDeck, PlayerManager playerManager)
        {
            playerManager.cardsPulled++;
            playerManager.playerCardsNum = handDeck.Count;
        }
    }
}
