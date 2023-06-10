using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using src.scripts.Hand;
using src.scripts.Managers;

public class CardPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    private Transform _unitParent;
    private TurnManager _turnManager;
    
    [SerializeField] private Merge merge;
    [SerializeField] private Puller puller;
    [SerializeField] private Hand hand;
    [SerializeField] private CardSelector cardSelector;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Discard discard;

    public void Start()
    {
        _unitParent = GameObject.Find("Units").transform;
        transform.SetParent(_unitParent);

        if (!photonView.IsMine)
            gameObject.SetActive(false);
        
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.AddPlayerToQueue(photonView.ViewID);
        _turnManager.cardPlayer = this;

        DeactivatePlayer();
    }

    public void ActivatePlayer()
    {
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
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_unitParent);
            stream.SendNext(_turnManager);
            stream.SendNext(merge);
            stream.SendNext(puller);
            stream.SendNext(hand);
            stream.SendNext(cardSelector);
            stream.SendNext(discard);
            stream.SendNext(playerManager);
        }
        else
        {
            _unitParent = (Transform)stream.ReceiveNext();
            _turnManager = (TurnManager)stream.ReceiveNext();
            merge = (Merge)stream.ReceiveNext();
            puller = (Puller)stream.ReceiveNext();
            hand = (Hand)stream.ReceiveNext();
            cardSelector = (CardSelector)stream.ReceiveNext();
            discard = (Discard)stream.ReceiveNext();
            playerManager = (PlayerManager)stream.ReceiveNext();
        }
    }
}
