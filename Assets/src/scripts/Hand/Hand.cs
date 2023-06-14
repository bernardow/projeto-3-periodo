using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
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
        public List<Transform> mesaPlaces = new List<Transform>();
        public static List<string> mesa1 = new List<string>();
        [Space(20)]
        
        [Header("Referencias Externas")]
        [SerializeField] private Transform cardsPos;
        public PlayerManager playerManager;
        public Trash trash;
        public Merge merge;
        public TurnManager turnManager;
        [Space(20)]
        
        [Header("Materiais")]
        public Material selectedMaterial;
        public Material defaultMaterial;
        [Space(20)]
        
        [Header("Mao")]
        [NotNull] public List<GameObject> player1Hand = new List<GameObject>();
        public Transform handTransform;
        public Camera playerCamera;
        

        //Classes Derivadas
        private Puller _puller;
        [SerializeField] private CardPlayer _cardPlayer;
        [HideInInspector] public CardSelector _cardSelector;
        private Discard _discard;
        

        [Header("Photon")] 
        public PhotonView photonViewPlayer;
                

        private void Start()
        {
            _puller = GetComponent<Puller>();
            _cardSelector = GetComponent<CardSelector>();
            _discard = GetComponent<Discard>();

            trash = GameObject.Find("Trash").GetComponent<Trash>();
            turnManager = FindObjectOfType<TurnManager>();
            
            _cardPlayer.DeactivatePlayer();
        }

        private void OnDisable()
        {
            if (_cardSelector.selectedCardsPlaye1.Count > 0)
            {
                foreach (var card in _cardSelector.selectedCardsPlaye1)
                {
                    Renderer render = GetChildComponent<Renderer>(card);
                    render.material = defaultMaterial;
                }
                _cardSelector.selectedCardsPlaye1.Clear();
            }
        }

        private void Update()
        {
            _puller.Pull(player1Hand, InGameDeck, cardsPos.position);
            _cardSelector.SelectCard(playerManager.CanSelect());
            _discard.DiscardCard(player1Hand, _cardSelector.selectedCardsPlaye1);
        }

        public void RearrangeCards() => _puller.PlaceCard(player1Hand, cardsPos.position);
    }
}
