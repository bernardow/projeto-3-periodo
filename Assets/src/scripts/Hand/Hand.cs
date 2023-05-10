using System.Collections.Generic;
using JetBrains.Annotations;
using src.scripts.Deck;
using src.scripts.Managers;
using UnityEngine;
using static src.scripts.Deck.Deck;
using static src.scripts.Hand.CardSelector;
using static src.scripts.FgLibrary;

namespace src.scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [Header("Referencias da Mesa")]
        [SerializeField] private List<Transform> mesaPlaces = new List<Transform>();
        public static List<string> mesa1 = new List<string>();
        [Space(20)]
        
        [Header("Referencias Externas")]
        [SerializeField] private Transform cardsPos;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Trash trash;
        [SerializeField] private Merge merge;
        [SerializeField] private TurnManager turnManager;
        [Space(20)]
        
        [Header("Materiais")]
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material defaultMaterial;
        [Space(20)]
        
        [Header("Mao")]
        [NotNull] protected static List<GameObject> player1Hand = new List<GameObject>();
        [SerializeField] protected Transform handTransform;

        //Classes Derivadas
        private Puller _puller;
        private CardSelector _cardSelector;
        private Discard _discard;
        private Dropper _dropper;

        private void Start()
        {
            _puller = new Puller();
            _cardSelector = new CardSelector();
            _discard = new Discard();
            _dropper = new Dropper();
        }

        private void OnDisable()
        {
            foreach (var card in selectedCardsPlaye1)
            {
                Renderer render = GetChildComponent<Renderer>(card);
                render.material = defaultMaterial;
            }
            selectedCardsPlaye1.Clear();
        }

        private void Update()
        {
            _puller.Pull(player1Hand, InGameDeck, cardsPos.position, playerManager);
            _cardSelector.SelectCard(selectedMaterial, defaultMaterial, playerManager.CanSelect());
            _discard.DiscardCard(selectedCardsPlaye1, trash, defaultMaterial, merge, this, playerManager, turnManager);
            _dropper.DropCard(player1Hand, selectedCardsPlaye1, playerManager, mesa1, mesaPlaces, defaultMaterial, turnManager);
        }

        public void RearrangeCards() => _puller.PlaceCard(player1Hand, cardsPos.position);
    }
}
