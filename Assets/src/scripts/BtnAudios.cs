using src.scripts.Managers;
using UnityEngine;

namespace src.scripts
{
    public class BtnAudios : MonoBehaviour
    {
        public void SendConfirmBtnAudio() => AudioManager.Instance.Play("ConfirmBtnEffect");

        public void SendDeniedBtnAudio() => AudioManager.Instance.Play("DeniedBtnEffect");
    }
}
