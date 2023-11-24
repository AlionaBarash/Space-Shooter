using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Func<int> onGameProcessEnd;

    public bool _isGameEnded { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        AudioManager.instance.PlayMusic(SoundName.GameTheme);
    }

    public void RestartGame() // before - save score
    {
        _isGameEnded = true;
        SetPause(false);

        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu() // before - save score
    {
        _isGameEnded = true;
        SetPause(false);

        SceneManager.LoadScene(0);
    }

    public void SetPause(bool isEnable)
    {
        Time.timeScale = isEnable ? 0 : 1;
    }
}
