using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CardPlayer : MonoBehaviourPunCallbacks
{
     public void Start()
    {
        if (!photonView.IsMine)
            gameObject.SetActive(false);
    }
}
