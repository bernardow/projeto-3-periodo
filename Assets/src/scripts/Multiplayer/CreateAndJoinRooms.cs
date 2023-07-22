using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace src.scripts.Multiplayer
{
    public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
    {
        public TMP_InputField roomNameInput;
        [SerializeField] private TMP_InputField nicknameField;
        [SerializeField] private Button startGameBtn;
        [SerializeField] private List<TextMeshProUGUI> nicknameTexts = new List<TextMeshProUGUI>();

        [Header("Screens References")]
        [SerializeField] private GameObject lobbyUI;
        [SerializeField] private GameObject roomUI;
    
        /// <summary>
        /// Creates a room
        /// </summary>
        public void CreateRoom() => PhotonNetwork.CreateRoom(roomNameInput.text);

        /// <summary>
        /// Joins Room
        /// </summary>
        public void JoinRoom() => PhotonNetwork.JoinRoom(roomNameInput.text);
        
        /// <summary>
        /// Updates player list
        /// </summary>
        public override void OnJoinedRoom()
        {
            ChangeUI(roomUI);
            photonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
            startGameBtn.interactable = PhotonNetwork.IsMasterClient;
        }

        /// <summary>
        /// Leaves Room
        /// </summary>
        public void LeaveRoom() => PhotonNetwork.LeaveRoom();

        /// <summary>
        /// Updates the texts of the display
        /// </summary>
        /// <param name="otherPlayer"></param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayerList();
            startGameBtn.interactable = PhotonNetwork.IsMasterClient;
        }
    
        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            if(PhotonNetwork.IsMasterClient)
                photonView.RPC("LoadLevel", RpcTarget.AllBuffered, "TestScene");
        }
        
        /// <summary>
        /// Changes the UI based on param
        /// </summary>
        /// <param name="currentUI">Target UI</param>
        private void ChangeUI(GameObject currentUI)
        {
            roomUI.SetActive(false);
            lobbyUI.SetActive(false);
        
            currentUI.SetActive(true);
        }

        /// <summary>
        /// Sets nickname
        /// </summary>
        public void SetNickName() => PhotonNetwork.NickName = nicknameField.text;
        
        #region RPCs
        
        /// <summary>
        /// Used to display witch player is playing
        /// </summary>
        [PunRPC]
        private void UpdatePlayerList()
        {
            foreach (var field in nicknameTexts)
                field.text = String.Empty;

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                nicknameTexts[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
        
        /// <summary>
        /// Load the level by name
        /// </summary>
        /// <param name="levelName">Level name</param>
        [PunRPC]
        public void LoadLevel(string levelName) => PhotonNetwork.LoadLevel("TestScene");
        
        #endregion
    }
}
