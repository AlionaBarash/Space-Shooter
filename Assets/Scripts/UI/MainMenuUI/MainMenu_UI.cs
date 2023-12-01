using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField]
    private Button _optionsButton;

    static public bool _isGameStart { get; private set; } = true;

    void Start()
    {
        AudioManager.instance.PlayMusic(SoundName.MainMenuTheme);

        if (_isGameStart)
        {
            _isGameStart = false;
        }
        else
        {
            _optionsButton.onClick.AddListener(OptionsWindow_UI.instance.ShowOptionsWindow);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
