using UnityEngine;
using UnityEngine.SceneManagement;

namespace src.scripts.Managers
{
    public class LocalSceneManager : MonoBehaviour
    {
        /// <summary>
        /// Changes the scene to the indicated by the index
        /// </summary>
        /// <param name="index">Index of the new scene</param>
        public void ChangeScene(int index) => SceneManager.LoadScene(index);
    }
}
