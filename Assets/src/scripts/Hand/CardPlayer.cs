using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using src.scripts.Hand;
using src.scripts.Managers;

public class CardPlayer : MonoBehaviourPunCallbacks
{
    public int life = 10;
    
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
        
        if(_turnManager.cardPlayer == null)
            _turnManager.cardPlayer = this;
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
}
