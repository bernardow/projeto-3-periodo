using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public static Stack<Vector3> playersPos = new Stack<Vector3>();

    private void Start()
    {
        playersPos.Push(new Vector3(1,1,1));
        playersPos.Push(new Vector3(2,2,2));
    
        PhotonNetwork.Instantiate(playerPrefab.name, playersPos.Pop(), Quaternion.identity);
    }
}
