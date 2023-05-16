using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    [SerializeField] private Vector3 player1Pos;
    [SerializeField] private Vector3 player2Pos;
    [SerializeField] private Vector3 player3Pos;
    [SerializeField] private Vector3 player4Pos;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, player1Pos, Quaternion.identity);
    }
}
