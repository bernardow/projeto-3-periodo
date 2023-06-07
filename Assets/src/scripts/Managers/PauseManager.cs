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
            if(cardPlayer.photonView.IsMine)
                _turnManager.playersInRoom.Remove(cardPlayer);
        }
        _turnManager.InitializeSetup();
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.LeaveRoom();
    }
}