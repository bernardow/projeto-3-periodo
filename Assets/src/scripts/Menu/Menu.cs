using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject optionsScreen;
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        menuScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void Return()
    {
        optionsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    private void Start()
    {
        AudioManager.Instance.Play("MenuTheme");
    }
}
