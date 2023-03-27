using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TurnManager : MonoBehaviour
{
    public List<string> turn = new List<string>();
    private string p1Turn = "Player 1";
    private string p2Turn = "Player 2";

    [SerializeField] private GameObject p1 = null;
    [SerializeField] private GameObject p2 = null;

    private void Start()
    {
        turn.Add(p2Turn);
        turn.Add(p1Turn);
    }

    public void SkipTurn()
    {
        turn.Reverse();
        TurnManagement();
    }

    private void TurnManagement()
    {
        if (FirstInQueue(turn) == p1Turn)
        {
            p1.SetActive(true);
            p2.SetActive(false);
        }
        else
        {
            p1.SetActive(false);
            p2.SetActive(true);
        }
    }
    
    private string FirstInQueue(List<string> list) => list[list.Count - 1];
    
}
