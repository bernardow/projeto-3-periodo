using System.Collections.Generic;
using JetBrains.Annotations;
using src.scripts.Deck;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.Deck.Deck;
using static src.scripts.Hand.CardSelector;

namespace src.scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Transform cardsPos;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Trash trash;
        [SerializeField] private Merge merge;

        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material defaultMaterial;
        
        [NotNull] protected static List<GameObject> player1Hand = new List<GameObject>();
        [SerializeField]protected Transform handTransform;

        private Puller _puller;
        private CardSelector _cardSelector;
        private Discard _discard;

        private void Start()
        {
            _puller = new Puller();
            _cardSelector = new CardSelector();
            _discard = new Discard();
        }

        private void Update()
        {
            _puller.Pull(player1Hand, InGameDeck, cardsPos.position, playerManager);
            _cardSelector.SelectCard(selectedMaterial, defaultMaterial, playerManager.CanSelect());
            _discard.DiscardCard(selectedCardsPlaye1, trash, defaultMaterial, merge, this);
        }

        public void RearrangeCards()
        {
            _puller.PlaceCard(player1Hand, cardsPos.position);
        }
    }
}
