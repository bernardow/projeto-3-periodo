using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace src.scripts.Multiplayer
{
    /// <summary>
    /// Spawn the players
    /// </summary>
    public class SpawnPlayers : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;

        public List<float> playersPos = new List<float>();
    
        private void Start()
        {
            Player player = PhotonNetwork.LocalPlayer;
            GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.Euler(0, playersPos[player.ActorNumber - 1], 0));
            if (newPlayer.GetComponent<PhotonView>().IsMine)
            {
                //Used to avoid self attacks and camera flicks
                GameObject model = newPlayer.transform.GetChild(2).gameObject;
                GameObject canvas = newPlayer.transform.GetChild(3).gameObject;
                model.layer = 3;
                model.GetComponent<Collider>().enabled = false;

                //Set to invisible to not render in player camera
                canvas.layer = 3;
                canvas.transform.GetChild(0).gameObject.layer = 3;
            }
        }
    }
}
