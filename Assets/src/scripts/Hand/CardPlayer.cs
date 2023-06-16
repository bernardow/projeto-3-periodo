using UnityEngine;
using Photon.Pun;
using src.scripts.Deck;
using src.scripts.Hand;
using src.scripts.Managers;
using static src.scripts.Deck.Deck;

public class CardPlayer : MonoBehaviourPunCallbacks
{
    public int life = 10;
    
    private Transform _unitParent;
    private TurnManager _turnManager;

    private GameManager _gameManager;

    private Vector3 _initalHandPos;
    
    [SerializeField] private Merge merge;
    [SerializeField] private Puller puller;
    [SerializeField] private Hand hand;
    [SerializeField] private CardSelector cardSelector;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Discard discard;
    [SerializeField] private RaycastManager raycastManager;

    public void Start()
    {
        _unitParent = GameObject.Find("Units").transform;
        transform.SetParent(_unitParent);

        _initalHandPos = hand.cardsPos.position;
        
        if (!photonView.IsMine)
            gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.AddPlayerToQueue(photonView.ViewID);

        if(_turnManager.cardPlayer == null)
            _turnManager.cardPlayer = this;

        _gameManager = FindObjectOfType<GameManager>();
    }

    public void ActivatePlayer()
    {
        raycastManager.enabled = true;
        merge.enabled = true;
        puller.enabled = true;
        hand.enabled = true;
        cardSelector.enabled = true;
        discard.enabled = true;
        playerManager.enabled = true;
    }
    
    public void DeactivatePlayer()
    {
        merge.enabled = false;
        puller.enabled = false;
        hand.enabled = false;
        cardSelector.enabled = false;
        discard.enabled = false;
        playerManager.enabled = false;
        raycastManager.enabled = false;
    }

    public void CheckLife()
    {
        if (life <= 0)
        {
            photonView.RPC("RemoveFromQueue", RpcTarget.All, photonView.ViewID);
            
            enabled = false;
        }
    }

    [PunRPC]
    private void RemoveFromQueue(int id)
    {
        _turnManager.playersInRoom.Remove(id);
        _gameManager.CheckForWinners(_turnManager.playersInRoom);
    }

    
    [PunRPC]
    private void PopCard()
    {
        InGameDeck.Pop();
    }
    
    [PunRPC]
    private void PlaceCardsGlobal()
    {
        hand.cardsPos.position = _initalHandPos;
        foreach (var card in hand.player1Hand)
        {
            card.transform.position = hand.cardsPos.position;
            card.transform.LookAt(hand.playerCamera!.transform.position);
            //card.transform.rotation = Quaternion.Euler(0, 90, 0);
            card.tag = "MyCards";
            card.GetComponent<Transform>().SetParent(hand.handTransform);
            hand.cardsPos.position += Vector3.left + Vector3.up * 0.3f;
        }
    }
    
    [PunRPC]
    public void DealDamage(int id)
    {
        GameObject targetPlayer = PhotonView.Find(id).gameObject;
        CardPlayer targetCardPlayer = targetPlayer.GetComponent<CardPlayer>();
        targetCardPlayer.life--;
        targetCardPlayer.CheckLife();
    }
}
