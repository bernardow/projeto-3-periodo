using System.Collections.Generic;
using src.scripts.Deck;
using UnityEngine;

namespace src.scripts
{
    public static class FgLibrary
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
            Joker,
            DoubleDamage,
            DrawCards
        }

        public static Stack<GameObject> ConvertToStack(List<GameObject> list)
        {
            Stack<GameObject> newStack = new Stack<GameObject>();
            foreach (var item in list)
                newStack.Push(item);

            return newStack;
        }

        public static T GetChildComponent<T>(GameObject gameObject)
        {
            T[] components = gameObject.GetComponentsInChildren<T>();
            return components[1];
        }

        public static List<T> GetChildComponents<T>(List<GameObject> gameObjects)
        {
            List<T> allComponents = new List<T>();
            List<T> childComponents = new List<T>();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                allComponents.Add(gameObjects[i].GetComponentInChildren<T>());
                    
                
            }
            return childComponents;
        }

        public static void DebugList<T>(List<T> list)
        {
            foreach (var item in list)
                Debug.Log(item.Equals(nameof(item)));
        }
    }
}
