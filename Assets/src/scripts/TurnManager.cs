using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TurnManager : MonoBehaviour
{
    private Stack turn = new Stack();
    private string p1Turn = "Vez 1";
    private string p2Turn = "Vez 2";

    private void Start()
    {
        turn.Push(p2Turn);
        turn.Push(p1Turn);
    }

    public void CheckTurn()
    {
        Debug.Log(turn.Peek());
    }
}
