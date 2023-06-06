using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using src.scripts.Managers;

public class CardPlayer : MonoBehaviourPunCallbacks
{
    private TurnManager turnManager;

     public void Start()
     {
        if (!photonView.IsMine)
            gameObject.SetActive(false);

        turnManager = FindObjectOfType<TurnManager>();
        turnManager.p1 = gameObject;
     }
}
