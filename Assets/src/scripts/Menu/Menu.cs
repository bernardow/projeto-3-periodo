using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        AudioManager.Instance.Play("MenuTheme");
    }
}
