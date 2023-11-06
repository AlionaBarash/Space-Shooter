using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action onPressingEscape;

    void OnEnable()
    {
        UIManager.onGamePause += SetPause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPressingEscape?.Invoke();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPause(bool isEnable)
    {
        Time.timeScale = isEnable ? 0 : 1;
    }

    void OnDisable()
    {
        UIManager.onGamePause -= SetPause;
    }
}
