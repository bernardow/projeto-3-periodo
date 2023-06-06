using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreeen;
    
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
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.LeaveRoom();
    }
}
