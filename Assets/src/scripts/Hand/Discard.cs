using System.Collections.Generic;
using src.scripts.Deck;
using src.scripts.Managers;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Discard : Hand
    {
        public void DiscardCard(List<GameObject> selectedCards, Trash trash, Material defaulMat, Merge merge, Hand hand, PlayerManager playerManager, TurnManager turnManager)
        {
            Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider.CompareTag("Trash") && selectedCards.Count is > 0 and < 2)
                {
                    trash.MoveToTrash(selectedCards[0], player1Hand, selectedCards, playerManager, defaulMat);
                    turnManager.SkipTurn();
                }
                else if (hit.collider.CompareTag("Trash") && selectedCards.Count > 1)
                {
                    List<GameObject> selectedCardsArray = new List<GameObject>();
                    foreach (var card in selectedCards)
                        selectedCardsArray.Add(card);

                    FgLibrary.CardsType mergedColor;
                    merge.CheckMergePossibilities(selectedCards[0], selectedCards[1], out mergedColor);
                    if (mergedColor != FgLibrary.CardsType.Joker && playerManager.CanMerge())
                    {
                        trash.MoveMergedCardsToTrahs(selectedCardsArray, player1Hand, selectedCards, playerManager, defaulMat);
                        merge.GetMergedColor(mergedColor, player1Hand, hand);
                        playerManager.mergedCards++;
                    }
                    
                }
            }
        }
    }
}
