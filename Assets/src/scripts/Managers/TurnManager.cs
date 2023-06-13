using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace src.scripts.Managers
{
    public class TurnManager : MonoBehaviourPunCallbacks
    {
        public List<int> playersInRoom = new List<int>();

        public int counter;
        private bool _initialized;

        public CardPlayer cardPlayer;
        
        private void Update()
        {
            if (!_initialized && counter == PhotonNetwork.PlayerList.Length && PhotonNetwork.IsMasterClient)
            {
                InitializeSetup();
                _initialized = true;
            }
            
        }

        public void InitializeSetup()
        {
            photonView.RPC("ActivateTopPlayer", RpcTarget.All, playersInRoom[0]);
        }

        public void ChangeTurnButtonAction()
        {
            photonView.RPC("SkipTurn", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void SkipTurn()
        {
            playersInRoom.Add(playersInRoom[0]);
            if(playersInRoom[0] == cardPlayer.GetComponent<PhotonView>().ViewID)
                cardPlayer.DeactivatePlayer();
            playersInRoom.Remove(playersInRoom[0]);
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("ActivateTopPlayer", RpcTarget.All, playersInRoom[0]);  
            }
            Debug.Log("Trocou");
        }

        [PunRPC]
        public void ActivateTopPlayer(int id)
        {
            Debug.Log(cardPlayer.GetComponent<PhotonView>().ViewID);
            if (id == cardPlayer.GetComponent<PhotonView>().ViewID)
            {
                cardPlayer.ActivatePlayer();
            }
        }

        public void AddPlayerToQueue(int id)
        {
            playersInRoom.Add(id);
            counter++;
        }
    }
}
