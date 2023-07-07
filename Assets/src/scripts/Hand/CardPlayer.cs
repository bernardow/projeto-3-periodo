using System.Collections;
using UnityEngine;
using Photon.Pun;
using src.scripts.Deck;
using src.scripts.Hand;
using src.scripts.Managers;
using static src.scripts.Deck.Deck;

public class CardPlayer : MonoBehaviourPunCallbacks, ICardPosObserver
{
    public int life = 10;
    public int bonus = 1;

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

    [SerializeField] private float forwardOffset;
    [SerializeField] private float downOffset;
    [SerializeField] private float rightOffset;

    public void Start()
    {
        _unitParent = GameObject.Find("Units").transform;
        transform.SetParent(_unitParent);

        hand.cardsPos.position = hand.playerCamera.transform.position + (hand.playerCamera.transform.forward * forwardOffset) - (hand.playerCamera.transform.up * downOffset) - (hand.playerCamera.transform.right * rightOffset);
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
        if (InGameDeck.Count == 0 && PhotonNetwork.IsMasterClient)
        {
            Deck deck = FindObjectOfType<Deck>();
            deck.deckPhotonView.RPC("NotifyDeck", RpcTarget.MasterClient);
        }
            
    }
    
    [PunRPC]
    private void PlaceCardsGlobal()
    {
        hand.cardsPos.position = _initalHandPos;
        foreach (var card in hand.player1Hand)
        {
            card.transform.position = hand.cardsPos.position;
            card.transform.LookAt(-hand.playerCamera!.transform.position);
            //card.transform.rotation = Quaternion.Euler(0, 90, 0);
            card.tag = "MyCards";
            card.GetComponent<Transform>().SetParent(hand.handTransform);
            hand.cardsPos.position += -hand.cardsPos.right * 5f;
        }
    }
    
    [PunRPC]
    public void DealDamage(int id, int damage)
    {
        GameObject targetPlayer = PhotonView.Find(id).gameObject;
        CardPlayer targetCardPlayer = targetPlayer.GetComponent<CardPlayer>();
        targetCardPlayer.life -= damage * bonus;
        targetCardPlayer.CheckLife();

        StartCoroutine(ShakeCamera(targetPlayer));

    }
    
    [PunRPC]
    public void DiscardTwo(int id)
    {
        GameObject player = PhotonView.Find(id).gameObject;
        Hand playerHand = player.GetComponentInChildren<Hand>();
        if (player.GetComponent<PhotonView>().ViewID == id)
        {
            playerHand.trash.MoveToTrash(playerHand.player1Hand[Random.Range(0, playerHand.player1Hand.Count)], playerHand.player1Hand, playerHand._cardSelector.selectedCardsPlaye1, playerHand.playerManager);
            playerHand.trash.MoveToTrash(playerHand.player1Hand[Random.Range(0, playerHand.player1Hand.Count)], playerHand.player1Hand, playerHand._cardSelector.selectedCardsPlaye1, playerHand.playerManager);
        }
    }

    [PunRPC]
    public void SyncCardPos(float x, float y, float z, float xRotation, float yRotation, float zRotation, float wRotation, int  id)
    {
        GameObject card = PhotonView.Find(id).gameObject;
        card.transform.position = new Vector3(x, y, z);
        card.transform.rotation = new Quaternion(xRotation, yRotation, zRotation, wRotation);
    }

    public void OnNotify(float x, float y, float z, float xRotation, float yRotation, float zRotation, float wRotation, int  id)
    {
        photonView.RPC("SyncCardPos", RpcTarget.All, x, y, z, xRotation, yRotation, zRotation, wRotation, id);
    }

    public IEnumerator ShakeCamera(GameObject player)
    {
        player.transform.GetChild(1).GetComponent<Animator>().SetBool("Shake", true);
        yield return new WaitForSeconds(0.25f);
        player.transform.GetChild(1).GetComponent<Animator>().SetBool("Shake", false);
    }
}
