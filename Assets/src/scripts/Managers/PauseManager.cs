using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using src.scripts.Managers;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreeen;
    private TurnManager _turnManager;

    private void Start() => _turnManager = FindObjectOfType<TurnManager>();

    public void Pause()
    {
        pauseScreeen.SetActive(!pauseScreeen.activeSelf);
    }
    
    public void ResumeGame()
    {
        pauseScreeen.SetActive(false);
    }

    public void QuitGame()
    {
        foreach (var cardPlayer in FindObjectsOfType<CardPlayer>())
        {
            if(cardPlayer.GetComponent<PhotonView>().IsMine)
                _turnManager.playersInRoom.Remove(cardPlayer.GetComponent<PhotonView>().ViewID);
        }
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.LeaveRoom();
    }
}
