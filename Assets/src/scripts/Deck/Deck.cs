using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using static src.scripts.Extensions;
using static src.scripts.Hand.Merge;

namespace src.scripts.Deck
{
    public class Deck : MonoBehaviourPunCallbacks
    {
        #region Proprieties
        
        [SerializeField] private List<Card> cards = new List<Card>();
        [SerializeField] private int numOfRedCards = 20;
        [SerializeField] private int numOfYellowCards = 12;
        [SerializeField] private int numOfBlueCards = 16;
        [SerializeField] private int numOfDoubleDmgCards = 8;
        [SerializeField] private int numOfDrawCardsCards = 8;
        [SerializeField] private int numOfForceDiscardCards = 8;
        [SerializeField] private int numOfRainbowDamageCards = 4;
        public List<GameObject> gameDeck;
        public static Stack<GameObject> InGameDeck;

        [Header("Cartas")] //Prefabs 
        [SerializeField] private GameObject redCard;
        [SerializeField] private GameObject yellowCard;
        [SerializeField] private GameObject blueCard;
        [SerializeField] private GameObject greenCard;
        [SerializeField] private GameObject purpleCard;
        [SerializeField] private GameObject orangeCard;
        [SerializeField] private GameObject pinkCard;
        [SerializeField] private GameObject cianCard;
        [SerializeField] private GameObject brownCard;
        [SerializeField] private GameObject doubleDmgCard;
        [SerializeField] private GameObject drawCardsCard;
        [SerializeField] private GameObject forceDiscardCard;
        [SerializeField] private GameObject rainbowDamageCard;
        
        [Header("Lugares das Cartas")]
        [SerializeField] private Transform greenCardPlace;
        [SerializeField] private Transform purpleCardPlace;
        [SerializeField] private Transform orangeCardPlace;
        [SerializeField] private Transform pinkCardPlace;
        [SerializeField] private Transform cianCardPlace;
        [SerializeField] private Transform brownCardPlace;

        [Header("Card Object")] //ScriptableObjects
        [SerializeField] private Card redCardObj;
        [SerializeField] private Card yellowCardObj;
        [SerializeField] private Card blueCardObj;
        [SerializeField] private Card doubleDmgCardObj;
        [SerializeField] private Card drawCardsCardObj;
        [SerializeField] private Card forceDiscardCardObj;
        [SerializeField] private Card rainbowDamageCardObj;

        private Vector3 _spawnPos;

        public PhotonView DeckPhotonView { get; private set; }
        
        #endregion
    
        // Initialize the Deck
        private void Awake()
        {
            DeckPhotonView = photonView;
            
            //MasterClient initializes all the cards in the deck
            if (PhotonNetwork.IsMasterClient)
                Initialize();

            //Each one of the players spawn special colors todo Make only the MasterClient initialize the Special Cards
            SpawnSpecialColors();
        }

        private void Initialize()
        {
            // Set the Spawn point to it`s origin
            _spawnPos = transform.position;
            
            //Add all the base cards in deck list
            AddCards(numOfRedCards, redCardObj);    
            AddCards(numOfBlueCards, blueCardObj);
            AddCards(numOfYellowCards, yellowCardObj);
            AddCards(numOfDoubleDmgCards, doubleDmgCardObj);
            AddCards(numOfDrawCardsCards, drawCardsCardObj);
            AddCards(numOfForceDiscardCards, forceDiscardCardObj);
            AddCards(numOfRainbowDamageCards, rainbowDamageCardObj);
            
            //Shuffle the list
            cards.Shuffle();
            
            //Spawn the cards in deck and special colors
            SpawnCards(cards);
            SpawnSpecialColors();
            
            //Notify Player Hands to convert the list to a Stack
            photonView.RPC("NotifyPlayersHands", RpcTarget.All);
        }

        #region Internal Methods

        /// <summary>
        /// Add cards to the list of the deck
        /// </summary>
        /// <param name="numOfCards">Number of that Card</param>
        /// <param name="cardObj">ScriptableObject of the Card</param>
        private void AddCards(int numOfCards, Card cardObj)
        {
            for (int i = 0; i < numOfCards; i++)
                cards.Add(cardObj);
        }

        /// <summary>
        /// Spawn the Cards in the Deck List of Card
        /// </summary>
        /// <param name="deck">List of Card</param>
        private void SpawnCards(List<Card> deck)
        {
            //Initializes the cardPrefab
            GameObject cardPrefab = new GameObject();

            //Iterates trough all the Card in deck and spawn the correct card for the specific CardsType propriety 
            foreach (Card card in deck)
            {
                switch (card.cardType)
                {
                    case CardsType.Red: cardPrefab = redCard;
                        break;
                    case CardsType.Yellow: cardPrefab = yellowCard;
                        break;
                    case CardsType.Blue: cardPrefab = blueCard;
                        break;
                    case CardsType.DoubleDamage: cardPrefab = doubleDmgCard;
                        break;
                    case CardsType.DrawCards: cardPrefab = drawCardsCard;
                        break;
                    case CardsType.ForceDiscard: cardPrefab = forceDiscardCard;
                        break;
                    case CardsType.RainbowDamage: cardPrefab = rainbowDamageCard;
                        break;
                    default:  Debug.LogError("Erro na tentativa de encontrar o tipo da carta");
                        break;
                }
            
                //Photon instantiate and pair their info to everybody in the room
                GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, _spawnPos, Quaternion.Euler(new Vector3(270, 0,0))).gameObject;
                photonView.RPC("PairCardInfo", RpcTarget.All, newCard.GetComponent<PhotonView>().ViewID);
            }
        }
        
        /// <summary>
        /// Spawn all of the SpecialColors in game (Kind of auto explain)
        /// </summary>
        private void SpawnSpecialColors()
        {
            SpawnColor(greenCardPlace.position, greenCard, 8, GreenCards);
            SpawnColor(purpleCardPlace.position, purpleCard, 8, PurpleCards);
            SpawnColor(orangeCardPlace.position, orangeCard, 8, OrangeCards);
            SpawnColor(pinkCardPlace.position, pinkCard, 4, PinkCards);
            SpawnColor(cianCardPlace.position, cianCard, 4, CianCards);
            SpawnColor(brownCardPlace.position, brownCard, 4, BrownCards);
        }

        /// <summary>
        /// Spawn a Color
        /// </summary>
        /// <param name="pos">Position of the new Card</param>
        /// <param name="prefab">Prefab of the new Card</param>
        /// <param name="numberOfCards">Number of Colors that must be spawn</param>
        /// <param name="colorStack">The Special Color Stack</param>
        private void SpawnColor(Vector3 pos, GameObject prefab, int numberOfCards, Stack<GameObject> colorStack)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                GameObject newColor = PhotonNetwork.Instantiate(prefab.name, pos, Quaternion.Euler(90,0, 0));
                pos += Vector3.up * 0.1f;
                colorStack.Push(newColor);
            }
        }
        
        #endregion

        #region RPCs
        
        /// <summary>
        /// Pair the Card info to everybody in the room
        /// </summary>
        /// <param name="id">ID of the new Card</param>
        [PunRPC]
        private void PairCardInfo(int id)
        {
            GameObject newCard = PhotonView.Find(id).gameObject;
            
            //Set parent to Deck GameObject
            newCard.transform.SetParent(transform);
            
            //Move the Spawn position a little upwards to make space for new cards
            _spawnPos += Vector3.up * 0.1f;
            
            //Set tag to "Deck" to make it interactable with the player
            newCard.tag = "Deck";
            
            gameDeck.Add(newCard);
        }
        
        [PunRPC]
        private void NotifyPlayersHands() => InGameDeck = gameDeck.ConvertToStack();
        
        [PunRPC]
        public void NotifyDeck() => Initialize();
        
        #endregion
    }
}
