using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FGLibrary
{
    public enum CardsType
    {
        Red,
        Yellow,
        Blue,
        Pink,
        Purple,
        Brown,
        Cian,
        Green,
        Orange,
        Joker
    }

    public static Stack<GameObject> ConvertToStack(List<GameObject> list)
    {
        Stack<GameObject> newStack = new Stack<GameObject>();
        foreach (var item in list)
            newStack.Push(item);

        return newStack;
    }
}
