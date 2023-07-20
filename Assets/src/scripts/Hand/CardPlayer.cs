using System.Collections;
using UnityEngine;
using Photon.Pun;
using src.scripts.CardsSync;
using src.scripts.Deck;
using src.scripts.Hand;
using src.scripts.Managers;
using static src.scripts.Deck.Deck;

public class CardPlayer : MonoBehaviourPunCallbacks, ICardPosObserver
{
    #region Proprieties

    [Header("Values")]
    public int life = 10;
    public int bonus = 1;
    
    private Vector3 _initalHandPos;

    //External references
    private Transform _unitParent;
    private TurnManager _turnManager;
    private GameManager _gameManager;

    [Header("Internal References")]//(Player scope)
    [SerializeField] private Hand hand;
    [SerializeField] private RaycastManager raycastManager;

    [Header("Offsets")]
    //Direction Offset of the shot
    [SerializeField] private float forwardOffset;
    [SerializeField] private float downOffset;
    [SerializeField] private float rightOffset;
    
    #endregion

    private void Start()
    {
        //Hierarchy Management
        _unitParent = GameObject.Find("Units").transform;
        transform.SetParent(_unitParent);

        //Caches and set cards of the player to the face of the camera
        Transform playerCamCached = hand.playerCamera.transform;
        hand.cardsPos.position = playerCamCached.position + (playerCamCached.forward * forwardOffset) - (playerCamCached.up * downOffset) - (playerCamCached.right * rightOffset);
        _initalHandPos = hand.cardsPos.position;

        //Disables camera if it`s not from the current player
        if (!photonView.IsMine)
        {
            gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(false);
            return;
        }
        
        //Add player to queue
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.AddPlayerToQueue(photonView.ViewID);

        //Set the cardPlayer to the current instance of the game in turn manager (That`s kinda weird but it`s working) todo find a better and safer way to make this
        if(_turnManager.cardPlayer == null)
            _turnManager.cardPlayer = this;

        //Assignment
        _gameManager = FindObjectOfType<GameManager>();
    }

    #region Public Methods

    /// <summary>
    /// Activate all important classes in player, making it playable
    /// </summary>
    public void ActivatePlayer()
    {
        raycastManager.enabled = true;
        hand.merge.enabled = true;
        hand.Puller.enabled = true;
        hand.enabled = true;
        hand.CardSelector.enabled = true;
        hand.Discard.enabled = true;
        hand.PlayerManager.enabled = true;
    }
    
    /// <summary>
    /// Deactivate all important classes in player, making it unplayable
    /// </summary>
    public void DeactivatePlayer()
    {
        hand.merge.enabled = false;
        hand.Puller.enabled = false;
        hand.enabled = false;
        hand.CardSelector.enabled = false;
        hand.Discard.enabled = false;
        hand.PlayerManager.enabled = false;
        raycastManager.enabled = false;
    }
    
    #endregion

    #region Internal Methods
    
    /// <summary>
    /// Check if player it`s dead and removes it from queue if it is
    /// </summary>
    private void CheckLife()
    {
        if (life <= 0)
        {
            photonView.RPC("RemoveFromQueue", RpcTarget.All, photonView.ViewID);
            enabled = false;
        }
    }
    
    /// <summary>
    /// Shakes the camera of the target
    /// </summary>
    /// <param name="player">Target player</param>
    /// <returns></returns>
    private IEnumerator ShakeCamera(GameObject player)
    {
        player.transform.GetChild(1).GetComponent<Animator>().SetBool("Shake", true);
        yield return new WaitForSeconds(0.25f);
        player.transform.GetChild(1).GetComponent<Animator>().SetBool("Shake", false);
    }
    
    #endregion

    #region RPCs
    
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
            deck.DeckPhotonView.RPC("NotifyDeck", RpcTarget.MasterClient);
        }
            
    }
    
    /// <summary>
    /// Place cards in the hand of the player to all players in the room to see
    /// </summary>
    [PunRPC]
    private void PlaceCardsGlobal()
    {
        hand.cardsPos.position = _initalHandPos;
        foreach (var card in hand.player1Hand)
        {
            card.transform.position = hand.cardsPos.position;
            card.transform.LookAt(-hand.playerCamera!.transform.position);
            card.tag = "MyCards";
            card.GetComponent<Transform>().SetParent(hand.handTransform);
            hand.cardsPos.position += -hand.cardsPos.right * 5f;
        }
    }

    /// <summary>
    /// Deal damage to a player
    /// </summary>
    /// <param name="id">ID of the target</param>
    /// <param name="damage">Amount of damage</param>
    [PunRPC]
    public void DealDamage(int id, int damage)
    {
        GameObject targetPlayer = PhotonView.Find(id).gameObject;
        CardPlayer targetCardPlayer = targetPlayer.GetComponent<CardPlayer>();
        targetCardPlayer.life -= damage;
        targetCardPlayer.CheckLife();

        StartCoroutine(ShakeCamera(targetPlayer));
    }

    /// <summary>
    /// Force the discard of two cards of a target
    /// </summary>
    /// <param name="id"></param>
    [PunRPC]
    public void DiscardTwo(int id)
    {
        GameObject player = PhotonView.Find(id).gameObject;
        Hand playerHand = player.GetComponentInChildren<Hand>();
        if (player.GetComponent<PhotonView>().ViewID == id)
        {
            playerHand.trash.MoveToTrash(playerHand.player1Hand[Random.Range(0, playerHand.player1Hand.Count)], playerHand);
            playerHand.trash.MoveToTrash(playerHand.player1Hand[Random.Range(0, playerHand.player1Hand.Count)], playerHand);
        }
    }

    /// <summary>
    /// Set the position of the card and sync with all players in game 
    /// </summary>
    /// <param name="x">x pos</param>
    /// <param name="y">y pos</param>
    /// <param name="z">z pos</param>
    /// <param name="xRotation">x rotation</param>
    /// <param name="yRotation">y rotation</param>
    /// <param name="zRotation">z rotation</param>
    /// <param name="wRotation">w rotation</param>
    /// <param name="id">ID of the card</param>
    [PunRPC]
    public void SyncCardPos(float x, float y, float z, float xRotation, float yRotation, float zRotation, float wRotation, int  id)
    {
        GameObject card = PhotonView.Find(id).gameObject;
        card.transform.SetPositionAndRotation(new Vector3(x, y, z), new Quaternion(xRotation, yRotation, zRotation, wRotation));
    }
    
    #endregion

    /// <summary>
    /// Sync card position and rotation trough all players
    /// </summary>
    /// <param name="x">x pos</param>
    /// <param name="y">y pos</param>
    /// <param name="z">z pos</param>
    /// <param name="xRotation">x rotation</param>
    /// <param name="yRotation">y rotation</param>
    /// <param name="zRotation">z rotation</param>
    /// <param name="wRotation">w rotation</param>
    /// <param name="id">ID of the card</param>
    public void OnNotify(float x, float y, float z, float xRotation, float yRotation, float zRotation, float wRotation,
        int id)
    {
        photonView.RPC("SyncCardPos", RpcTarget.All, x, y, z, xRotation, yRotation, zRotation, wRotation, id);
    }
}
