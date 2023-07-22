using Photon.Pun;
using UnityEngine;

namespace src.scripts.Managers
{
    public class PauseManager : MonoBehaviour
    {
        //Screens
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject tutorialScreen;
        [SerializeField] private GameObject optionsScreen;
        
        //References
        private TurnManager _turnManager;

        //Assignments
        private void Start() => _turnManager = FindObjectOfType<TurnManager>();

        /// <summary>
        /// Activates Pause screen todo make impossible for player to play while is with the game paused
        /// </summary>
        public void Pause() => pauseScreen.SetActive(!pauseScreen.activeSelf);
        
        /// <summary>
        /// Resumes Game
        /// </summary>
        public void ResumeGame() => pauseScreen.SetActive(false);
        
        /// <summary>
        /// Set active options scene
        /// </summary>
        public void Options()
        {
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(true);
        }

        /// <summary>
        /// Returns to pause scene
        /// </summary>
        public void Return()
        {
            optionsScreen.SetActive(false);
            pauseScreen.SetActive(true);
        }

        /// <summary>
        /// Quit game to main menu
        /// </summary>
        public void QuitGame()
        {
            foreach (var cardPlayer in FindObjectsOfType<CardPlayer>())
            {
                if (cardPlayer.GetComponent<PhotonView>().IsMine)
                {
                    PhotonView photonView = cardPlayer.GetComponent<PhotonView>();
                    _turnManager.playersInRoom.Remove(photonView.ViewID);
                }
            }
            
            AudioManager.Instance.Stop("MainTheme");
            PhotonNetwork.LoadLevel("Lobby");
            PhotonNetwork.LeaveRoom();
        }

        /// <summary>
        /// Show tutorial screen
        /// </summary>
        public void ShowTutorial() => tutorialScreen.SetActive(!tutorialScreen.activeSelf);
    
    }
}
