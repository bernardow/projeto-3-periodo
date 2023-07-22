using src.scripts.Managers;
using UnityEngine;

namespace src.scripts.Menu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject menuScreen;
        [SerializeField] private GameObject optionsScreen;
    
        /// <summary>
        /// Quit aplication
        /// </summary>
        public void Quit() => Application.Quit();

        /// <summary>
        /// Turns on the options screen
        /// </summary>
        public void Options()
        {
            menuScreen.SetActive(false);
            optionsScreen.SetActive(true);
        }

        /// <summary>
        /// Return to main menu screen
        /// </summary>
        public void Return()
        {
            optionsScreen.SetActive(false);
            menuScreen.SetActive(true);
        }

        //Play menu theme
        private void Start() => AudioManager.Instance.Play("MenuTheme");
        
    }
}
