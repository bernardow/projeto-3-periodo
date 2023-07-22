using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public static ConnectToServer Instance { get; private set; }
    
    //Sets singleton
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

    //Connect with the server
    void Start() => PhotonNetwork.ConnectUsingSettings();

    //Joins lobby
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    //Load Menu scene
    public override void OnJoinedLobby() => SceneManager.LoadScene("Menu");
    
}
