using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace src.scripts.Managers
{
    public class TurnManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TextMeshProUGUI playerNameDisplay;

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
            cardPlayer.DeactivatePlayer();
            playersInRoom.Remove(playersInRoom[0]);
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("ActivateTopPlayer", RpcTarget.All, playersInRoom[0]);  
            }
        }

        [PunRPC]
        public void ActivateTopPlayer(int id)
        {
            if (id == cardPlayer.GetComponent<PhotonView>().ViewID)
            {
                cardPlayer.ActivatePlayer();
            }
            playerNameDisplay.transform.parent.gameObject.SetActive(true);
            playerNameDisplay.text = PhotonView.Find(playersInRoom[0]).Owner.NickName + "'s " + "turn";
        }

        public void AddPlayerToQueue(int id)
        {
            playersInRoom.Add(id);
            counter++;
        }
    }
}
