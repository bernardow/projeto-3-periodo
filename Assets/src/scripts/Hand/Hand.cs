using System.Collections.Generic;
using UnityEngine;
using static Deck;

namespace src.scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Transform cardsPos;
        protected List<GameObject> hand = new List<GameObject>();
        private Puller _puller;

        private void Start()
        {
            _puller = new Puller();
        }

        private void Update()
        {
            _puller.Pull(hand, InGameDeck, cardsPos.position);
        }
    }
}
