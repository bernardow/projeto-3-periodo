using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using src.scripts.Hand;

namespace src.scripts.Managers
{
    public class TurnManager : MonoBehaviourPunCallbacks
    {
        public List<CardPlayer> playersInRoom = new List<CardPlayer>();
        private Queue<CardPlayer> playersQueue = new Queue<CardPlayer>();
        
        public void InitializeSetup()
        {
             photonView.RPC("UpdatePlayersInRoom",  RpcTarget.AllBuffered);
             
        }

        public void ChangeTurnButtonAction()
        {
            photonView.RPC("SkipTurn",  RpcTarget.AllBuffered);
        }
        
        [PunRPC]
        public void SkipTurn()
        {
            playersQueue.Peek().DeactivatePlayer();
            playersQueue.Insert(playersQueue.Peek());
            playersQueue.Remove();
            playersQueue.Peek().ActivatePlayer();
        }

        [PunRPC]
        public void UpdatePlayersInRoom()
        {
            playersQueue.Clear();
            foreach (var player in playersInRoom)
            {
                playersQueue.Insert(player);
            }
            playersQueue.Peek().ActivatePlayer();
        }
    }
}
