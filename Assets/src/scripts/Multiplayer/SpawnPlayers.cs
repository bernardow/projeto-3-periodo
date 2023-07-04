using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public List<float> playersPos = new List<float>();
    
    private void Start()
    {
        Player player = PhotonNetwork.LocalPlayer;
        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.Euler(0, playersPos[player.ActorNumber - 1], 0));
        if (newPlayer.GetComponent<PhotonView>().IsMine)
        {
            GameObject model = newPlayer.transform.GetChild(2).gameObject;
            model.layer = 3;
            model.GetComponent<Collider>().enabled = false;
        }
    }
}
