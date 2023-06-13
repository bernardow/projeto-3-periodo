using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    [SerializeField] private TMP_InputField nicknameField;
    [SerializeField] private Button startGameBtn;
    [SerializeField] private List<TextMeshProUGUI> nicknameTexts = new List<TextMeshProUGUI>();

    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject roomUI;
    
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomNameInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomNameInput.text);
    }

    public override void OnJoinedRoom()
    {
        ChangeUI(roomUI);
        photonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
        startGameBtn.interactable = PhotonNetwork.IsMasterClient;
    }

    [PunRPC]
    private void UpdatePlayerList()
    {
        foreach (var field in nicknameTexts)
        {
            field.text = String.Empty;
        }
        
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            nicknameTexts[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        startGameBtn.interactable = PhotonNetwork.IsMasterClient;
    }
    

    public void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
            photonView.RPC("LoadLevel", RpcTarget.AllBuffered, "TestScene");
    }

    [PunRPC]
    public void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel("TestScene");
    }

    private void ChangeUI(GameObject currentUI)
    {
        roomUI.SetActive(false);
        lobbyUI.SetActive(false);
        
        currentUI.SetActive(true);
    }

    public void SetNickName()
    {
        PhotonNetwork.NickName = nicknameField.text;
    }
}
