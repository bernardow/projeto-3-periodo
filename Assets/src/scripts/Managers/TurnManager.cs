using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using src.scripts.Hand;
using Unity.VisualScripting;

namespace src.scripts.Managers
{
    public class TurnManager : MonoBehaviourPunCallbacks
    {
        public List<CardPlayer> playersInRoom = new List<CardPlayer>();
        private Queue<CardPlayer> playersQueue = new Queue<CardPlayer>();

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
            UpdatePlayersInRoom();
        }

        public void ChangeTurnButtonAction()
        {
            photonView.RPC("SkipTurn", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void SkipTurn()
        {
            playersQueue.Peek().DeactivatePlayer();
            playersQueue.Insert(playersQueue.Peek());
            playersQueue.Remove();
            playersQueue.Peek().ActivatePlayer();
        }

        public void UpdatePlayersInRoom()
        {
            if (PhotonNetwork.IsMasterClient && counter == PhotonNetwork.PlayerList.Length)
            {
                playersQueue.Clear();
                foreach (var player in playersInRoom)
                {
                    playersQueue.Insert(player);
                }

                photonView.RPC("ActivatePeekPlayer", RpcTarget.AllBuffered);
            }
        }

        public void AddPlayerToQueue(CardPlayer cardPlayer)
        {
            playersInRoom.Add(cardPlayer);
            counter++;
        }

        [PunRPC]
        public void ActivatePeekPlayer()
        {
            playersQueue.Peek().ActivatePlayer();
        }
    }
}
