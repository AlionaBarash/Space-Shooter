using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void RestartGame()
    {
        SetPause(false);

        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SetPause(false);

        SceneManager.LoadScene(0);
    }

    public void SetPause(bool isEnable)
    {
        Time.timeScale = isEnable ? 0 : 1;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
