using System.Collections.Generic;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.Deck.Deck;

namespace src.scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Transform cardsPos;
        [SerializeField] private PlayerManager playerManager;

        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material defaultMaterial;
        
        protected List<GameObject> hand = new List<GameObject>();
        
        private Puller _puller;
        private CardSelector _cardSelector;

        private void Start()
        {
            _puller = new Puller();
            _cardSelector = new CardSelector();
        }

        private void Update()
        {
            _puller.Pull(hand, InGameDeck, cardsPos.position, playerManager);
            _cardSelector.SelectCard(selectedMaterial, defaultMaterial);
        }
    }
}
