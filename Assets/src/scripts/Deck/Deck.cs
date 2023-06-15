using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Hand;
using UnityEngine;
using Random = UnityEngine.Random;
using static src.scripts.FgLibrary;
using static src.scripts.Hand.Merge;

namespace src.scripts.Deck
{
    public class Deck : MonoBehaviourPunCallbacks
    {
        [SerializeField] private List<Card> cards = new List<Card>();
        [SerializeField] private int numOfRedCards = 20;
        [SerializeField] private int numOfYellowCards = 12;
        [SerializeField] private int numOfBlueCards = 16;
        public List<GameObject> gameDeck;
        public static Stack<GameObject> InGameDeck;

        [Header("Cartas")] 
        [SerializeField] private GameObject redCard;
        [SerializeField] private GameObject yellowCard;
        [SerializeField] private GameObject blueCard;
        [SerializeField] private GameObject greenCard;
        [SerializeField] private GameObject purpleCard;
        [SerializeField] private GameObject orangeCard;
        [SerializeField] private GameObject pinkCard;
        [SerializeField] private GameObject cianCard;
        [SerializeField] private GameObject brownCard;
        
        [Header("Lugares das Cartas")]
        [SerializeField] private Transform greenCardPlace;
        [SerializeField] private Transform purpleCardPlace;
        [SerializeField] private Transform orangeCardPlace;
        [SerializeField] private Transform pinkCardPlace;
        [SerializeField] private Transform cianCardPlace;
        [SerializeField] private Transform brownCardPlace;

        [Header("Card Object")] 
        [SerializeField] private Card redCardObj;
        [SerializeField] private Card yellowCardObj;
        [SerializeField] private Card blueCardObj;

        public Hand.Hand _masterPlayer;
        public Merge _masterMerge;
        
        private Vector3 _spawnPos;
    
        // Start is called before the first frame update
        private void Awake()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (CardPlayer cardPlayer in FindObjectsOfType<CardPlayer>())
                {
                    if (cardPlayer.GetComponent<PhotonView>().IsMine)
                        photonView.RPC("SetMasterPlayer", RpcTarget.All, cardPlayer.GetComponent<PhotonView>().ViewID);
                }
                Initialize();
            }
        }
        
        private void Initialize()
        {
            _spawnPos = transform.position;
            AddCards(numOfRedCards, redCardObj);
            AddCards(numOfBlueCards, blueCardObj);
            AddCards(numOfYellowCards, yellowCardObj);
            ShuffleDeck(cards);
            SpawnCards(cards);
            photonView.RPC("NotifyPlayersHands", RpcTarget.All);
        }
        
        private void AddCards(int numOfCards, Card cardObj)
        {
            for (int i = 0; i < numOfCards; i++)
                cards.Add(cardObj);
        }


        private void ShuffleDeck(List<Card> deck)
        {
            // Embaralha o deck usando o algoritmo Fisher-Yates

            int deckSize = deck.Count;

            for (int i = deckSize - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1); // Gera um índice aleatório

                // Troca a posição das cartas no índice i e j
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }
        }

        private void SpawnCards(List<Card> deck)
        {
            GameObject cardPrefab = redCard;

            foreach (var card in deck)
            {
                switch (card.cardType)
                {
                    case CardsType.Red: cardPrefab = redCard;
                        break;
                    case CardsType.Yellow: cardPrefab = yellowCard;
                        break;
                    case CardsType.Blue: cardPrefab = blueCard;
                        break;
                    default:  Debug.LogError("Erro na tentativa de encontrar o tipo da carta");
                        break;
                }
            
                GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, _spawnPos, Quaternion.Euler(new Vector3(90, 0,0))).gameObject;
                photonView.RPC("PairCardInfo", RpcTarget.All, newCard.GetComponent<PhotonView>().ViewID);
            }
        }
        
   
        public void SpawnSpecialColors()
        {
            SpawnColor(greenCardPlace.position, greenCard, 8, _masterMerge.GreenCards);
            SpawnColor(purpleCardPlace.position, purpleCard, 8, _masterMerge.PurpleCards);
            SpawnColor(orangeCardPlace.position, orangeCard, 8, _masterMerge.OrangeCards);
            SpawnColor(pinkCardPlace.position, pinkCard, 4, _masterMerge.PinkCards);
            SpawnColor(cianCardPlace.position, cianCard, 4, _masterMerge.CianCards);
            SpawnColor(brownCardPlace.position, brownCard, 4, _masterMerge.BrownCards);
        }

        private void SpawnColor(Vector3 pos, GameObject prefab, int numberOfCards, Stack<GameObject> colorStack)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                GameObject newColor = PhotonNetwork.Instantiate(prefab.name, pos, Quaternion.Euler(90,0, 0));
                pos += Vector3.up * 0.1f;
                colorStack.Push(newColor);
            }
        }

        [PunRPC]
        private void SetMasterPlayer(int id)
        {
            _masterPlayer = PhotonView.Find(id).GetComponentInChildren<Hand.Hand>();
            _masterMerge = _masterPlayer.GetComponent<Merge>();
        }

        [PunRPC]
        private void PairCardInfo(int id)
        {
            GameObject newCard = PhotonView.Find(id).gameObject;
            newCard.transform.SetParent(transform);
            _spawnPos += Vector3.up * 0.1f;
            newCard.tag = "Deck";
            gameDeck.Add(newCard);
        }

        [PunRPC]
        private void SyncSpecialColorsStacks()
        {
            foreach (var cardPlayer in FindObjectsOfType<CardPlayer>())
            {
                if (cardPlayer.GetComponent<PhotonView>().IsMine)
                {
                    Merge myMerge = cardPlayer.GetComponentInChildren<Merge>();
                    myMerge.BrownCards = _masterMerge.BrownCards;
                    myMerge.BrownCards = _masterMerge.CianCards;
                    myMerge.BrownCards = _masterMerge.GreenCards;
                    myMerge.BrownCards = _masterMerge.OrangeCards;
                    myMerge.BrownCards = _masterMerge.PinkCards;
                    myMerge.BrownCards = _masterMerge.PurpleCards;
                }
            }
        }

        [PunRPC]
        private void NotifyPlayersHands()
        {
            InGameDeck = ConvertToStack(gameDeck);
        }
    }
}
