using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject looseScreen;

    private void Start()
    {
        AudioManager.Instance.Stop("MenuTheme");
        AudioManager.Instance.Play("MainTheme");
    }

    public void CheckForWinners(List<int> queue)
    {
        if (queue.Count < 2)
        {
            AudioManager.Instance.Stop("MainTheme");
            GameObject winner = PhotonView.Find(queue[0]).gameObject;
            PhotonView winnerPhotonVier = winner.GetComponent<PhotonView>();
            if (winnerPhotonVier.IsMine)
            {
                ActivateWinScreen();
                AudioManager.Instance.Play("GameOver");

                return;
            }
            AudioManager.Instance.Play("GameOverTheme");
            ActivateDefeatScreen();
        }
    }

    private void ActivateWinScreen()
    {
        winScreen.SetActive(true);
        AudioManager.Instance.Play("VictoryEffect");
    }

    private void ActivateDefeatScreen()
    {
        looseScreen.SetActive(true);
    }
}
