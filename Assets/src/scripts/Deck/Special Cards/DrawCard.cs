using System.Collections.Generic;
using src.scripts.Hand;
using UnityEngine;

public class DrawCard : MonoBehaviour, IObserverCard
{
    private List<CardPlayer> _players = new List<CardPlayer>();
    private List<int> _lifes = new List<int>();

    private void CheckForDraw(CardPlayer myPlayer)
    {
        if (_players.Count == 0)
            foreach (var cardPlayer in FindObjectsOfType<CardPlayer>())
            {
                _players.Add(cardPlayer);
                _lifes.Add(cardPlayer.life);
            }
        
        _lifes.Sort();

        if (_lifes[0] == myPlayer.life)
        {
            Draw(3, myPlayer);
            return;
        }
        Draw(2, myPlayer);
    }

    private void Draw(int numOfCards, CardPlayer myPlayer)
    {
        Hand myHand = myPlayer.GetComponentInChildren<Hand>();
        for(int i = 1; i <= numOfCards; i++)
            myHand.puller.PullCard(myHand);
    }
    
    public void OnNotify(CardPlayer player)
    {
        CheckForDraw(player);
    }
}
