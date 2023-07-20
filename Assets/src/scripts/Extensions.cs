using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace src.scripts
{
    /// <summary>
    /// Responsible for extensions and common access trough the CardsType enum
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Type of the Cards in game
        /// </summary>
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
            DrawCards,
            ForceDiscard,
            RainbowDamage
        }

        /// <summary>
        /// Converts a List of GameObjects into a Stack of GameObjects
        /// </summary>
        /// <param name="list"></param>
        /// <returns>Stack of GameObject</returns>
        public static Stack<GameObject> ConvertToStack(this List<GameObject> list)
        {
            Stack<GameObject> newStack = new Stack<GameObject>();
            foreach (var item in list)
                newStack.Push(item);

            return newStack;
        }

        /// <summary>
        /// Get the child component of the GameObject calling it
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>The component</returns>
        public static T GetChildComponent<T>(this GameObject gameObject)
        {
            T[] components = gameObject.GetComponentsInChildren<T>();
            return components[1];
        }

        /// <summary>
        /// Get the child components of the GameObject
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <param name="includeParent">Include parent component</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>List of the child objects</returns>
        public static List<T> GetChildComponents<T>(this GameObject gameObjects, bool includeParent = false)
        {
            T[] allComponents = gameObjects.GetComponentsInChildren<T>();
            List<T> childComponents = new List<T>();
            
            if (gameObjects.GetComponentInChildren<T>() == null || (gameObjects.GetComponentInChildren<T>() != null && includeParent))
                return allComponents.ToList();
            
            for (int i = 0; i < allComponents.Length; i++)
                    if(i != 0)
                        childComponents.Add(allComponents[i]); 
            
            return childComponents;
        }

        /// <summary>
        /// Do a Debug.Log of all the names in the list
        /// </summary>
        /// <param name="list">Current List</param>
        /// <typeparam name="T">type</typeparam>
        public static void DebugList<T>(this List<T> list)
        {
            foreach (var item in list)
                Debug.Log(item.Equals(nameof(item)));
        }

        /// <summary>
        /// Shuffles the List using Fisher-Yates algorithym
        /// </summary>
        /// <param name="list">Current List</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Shuffle<T>(this List<T> list)
        {
            // Embaralha o deck usando o algoritmo Fisher-Yates

            int listSize = list.Count;

            for (int i = listSize - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1); // Gera um índice aleatório

                // Troca a posição das cartas no índice i e j
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
