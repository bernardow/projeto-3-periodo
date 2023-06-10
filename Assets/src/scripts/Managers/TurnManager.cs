using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace src.scripts.Managers
{
    public class TurnManager : MonoBehaviourPunCallbacks
    {
        public List<CardPlayer> playersInRoom = new List<CardPlayer>();

        public int counter;
        private bool _initialized;
        
        private void Update()
        {
            if (!_initialized && counter == PhotonNetwork.PlayerList.Length)
            {
                InitializeSetup();
                _initialized = true;
            }
            
        }

        public void InitializeSetup()
        {
            if(PhotonNetwork.IsMasterClient)
                photonView.RPC("ActivateTopPlayer", RpcTarget.All);
        }

        public void ChangeTurnButtonAction()
        {
            photonView.RPC("SkipTurn", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void SkipTurn()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("ActivateTopPlayer", RpcTarget.All);  
            }
        }

        [PunRPC]
        public void ActivateTopPlayer()
        {
            playersInRoom[0].DeactivatePlayer();
            playersInRoom.Add(playersInRoom[0]);
            playersInRoom.Remove(playersInRoom[0]);
            playersInRoom[0].ActivatePlayer();
        }

        public void AddPlayerToQueue(CardPlayer cardPlayer)
        {
            playersInRoom.Add(cardPlayer);
            counter++;
        }
    }
}
