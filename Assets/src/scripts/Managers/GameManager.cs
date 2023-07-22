using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Managers
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject looseScreen;
        
        //Audio maagement between scenes
        private void Start()
        {
            AudioManager.Instance.Stop("MenuTheme");
            AudioManager.Instance.Play("MainTheme");
        }

        /// <summary>
        /// Verifies if there is another player in queue
        /// </summary>
        /// <param name="queue">Queue of the players</param>
        public void CheckForWinners(List<int> queue)
        {
            if (queue.Count < 2)
            {
                AudioManager.Instance.Stop("MainTheme");
                GameObject winner = PhotonView.Find(queue[0]).gameObject;
                PhotonView winnerPhotonVier = winner.GetComponent<PhotonView>();
                if (winnerPhotonVier.IsMine)
                {
                    ActivateWinScreen();
                    AudioManager.Instance.Play("GameOverEffect");

                    return;
                }
                AudioManager.Instance.Play("GameOverEffect");
                ActivateDefeatScreen();
            }
        }

        /// <summary>
        /// Activates Win Screen
        /// </summary>
        private void ActivateWinScreen()
        {
            winScreen.SetActive(true);
            AudioManager.Instance.Play("VictoryEffect");
        }

        /// <summary>
        /// Activates Defeat Screen
        /// </summary>
        private void ActivateDefeatScreen()
        {
            looseScreen.SetActive(true);
        }
    }
}
