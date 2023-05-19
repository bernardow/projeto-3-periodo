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
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        Player player = PhotonNetwork.LocalPlayer;
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.Euler(0, playersPos[player.ActorNumber - 1], 0)); 
    }
}
