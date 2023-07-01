using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAudios : MonoBehaviour
{
    public void SendConfirmBtnAudio() => AudioManager.Instance.Play("ConfirmBtnEffect");

    public void SendDeniedBtnAudio() => AudioManager.Instance.Play("DeniedBtnEffect");

}
