using System.Collections;
using System.Collections.Generic;
using src.scripts;
using src.scripts.Deck;
using src.scripts.Hand;
using UnityEngine;

public class Discard : Hand
{
    public void DiscardCard(List<GameObject> selectedCards, Trash trash, Material defaulMat, Merge merge)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.CompareTag("Trash") && selectedCards.Count is > 0 and < 2)
                trash.MoveToTrash(selectedCards[0], player1Hand, selectedCards, defaulMat);
            else if (hit.collider.CompareTag("Trash") && selectedCards.Count > 1)
            {
                List<GameObject> selectedCardsArray = new List<GameObject>();
                foreach (var card in selectedCards)
                    selectedCardsArray.Add(card);

                FgLibrary.CardsType mergedColor;
                merge.CheckMergePossibilities(selectedCards[0], selectedCards[1], out mergedColor);
                if (mergedColor != FgLibrary.CardsType.Joker)
                {
                    trash.MoveMergedCardsToTrahs(selectedCardsArray, player1Hand, selectedCards, defaulMat);
                    merge.GetMergedColor(mergedColor, player1Hand, this);
                }
                    
            }
        }
    }
}
