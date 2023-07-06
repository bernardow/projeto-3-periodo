using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using src.scripts.Deck;
using src.scripts.Managers;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreeen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject optionsScreen;
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

    public void Options()
    {
        pauseScreeen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void Return()
    {
        optionsScreen.SetActive(false);
        pauseScreeen.SetActive(true);
    }

    public void QuitGame()
    {
        PhotonView photonView = null;
        foreach (var cardPlayer in FindObjectsOfType<CardPlayer>())
        {
            if (cardPlayer.GetComponent<PhotonView>().IsMine)
            {
                photonView = cardPlayer.GetComponent<PhotonView>();
                _turnManager.playersInRoom.Remove(photonView.ViewID);
            }
        }
        
        
        AudioManager.Instance.Stop("MainTheme");
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.LeaveRoom();
    }

    public void ShowTutorial() => tutorialScreen.SetActive(!tutorialScreen.activeSelf);
    
}
