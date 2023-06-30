using System;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public static ConnectToServer Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby() => SceneManager.LoadScene("Menu");
    
}
