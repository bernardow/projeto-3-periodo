using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using src.scripts.Deck;
using src.scripts.Managers;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [Header("External References")]
        public Transform cardsPos;
        public Trash trash;
        public Merge merge;
        public TurnManager turnManager;
        [Space(20)]
        
        [Header("Hand")]
        [NotNull] public List<GameObject> player1Hand = new List<GameObject>();
        public Transform handTransform;
        public Camera playerCamera;
        

        //Classes Derivadas
        public Puller Puller { get; private set; }
        public CardPlayer CardPlayer { get; private set; }
        public CardSelector CardSelector { get; private set; }
        public Attack Attack { get; private set; }
        public Discard Discard { get; private set; }
        public PlayerManager PlayerManager { get; private set; }
        

        [Header("Photon")] 
        public PhotonView photonViewPlayer;
                
        //Assigns all the derived classes and deactivate the player
        private void Start()
        {
            Puller = GetComponent<Puller>();
            CardSelector = GetComponent<CardSelector>();
            Discard = GetComponent<Discard>();
            Attack = GetComponent<Attack>();
            CardPlayer = GetComponentInParent<CardPlayer>();
            PlayerManager = GetComponent<PlayerManager>();
            trash = GameObject.Find("Trash").GetComponent<Trash>();
            turnManager = FindObjectOfType<TurnManager>();
            
            CardPlayer.DeactivatePlayer();
        }
        
        private void OnDisable()
        {
            //Deselect the cards when disabled
            if (CardSelector.selectedCardsPlaye1.Count > 0)
            {
                foreach (GameObject card in CardSelector.selectedCardsPlaye1)
                {
                    GameObject outline = card.transform.GetChild(0).GetChild(0).gameObject;
                    outline.SetActive(false);
                }
                CardSelector.selectedCardsPlaye1.Clear();
            }
        }

        #region Public Methods
        
        /// <summary>
        /// Rearrange the cards in the hand of the player
        /// </summary>
        public void RearrangeCards() => Puller.PlaceCard();

        /// <summary>
        /// Removes a Card from the player (Kinda auto explain)
        /// </summary>
        /// <param name="card">Card that`s being removed</param>
        public void RemoveCardFromPlayer(GameObject card)
        {
            player1Hand.Remove(card);
            CardSelector.selectedCardsPlaye1.Remove(card);
        }
        #endregion
    }
}
