using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using static src.scripts.FgLibrary;

namespace src.scripts.Deck
{
    public class Deck : MonoBehaviour
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

        [Header("Card Object")] 
        [SerializeField] private Card redCardObj;
        [SerializeField] private Card yellowCardObj;
        [SerializeField] private Card blueCardObj;
    
        private Vector3 _spawnPos;
    
        // Start is called before the first frame update
        private void Awake()
        {
            _spawnPos = transform.position;
            AddCards(numOfRedCards, redCardObj);
            AddCards(numOfBlueCards, blueCardObj);
            AddCards(numOfYellowCards, yellowCardObj);
            ShuffleDeck(cards);
            SpawnCards(cards);
            NotifyPlayersHands();
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
            
                GameObject newCard = Instantiate(cardPrefab, _spawnPos, Quaternion.Euler(new Vector3(90,0,0)), transform).gameObject;
                _spawnPos += Vector3.up * 0.1f;
                gameDeck.Add(newCard);
            }
        }

        private void NotifyPlayersHands()
        {
            InGameDeck = ConvertToStack(gameDeck);
        }
    }
}
