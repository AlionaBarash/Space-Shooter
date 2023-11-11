using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action onPressingEscape;

    void Start()
    {
        PauseWindow_UI.onPauseGame += SetPause; 
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
        SetPause(false);

        SceneManager.LoadScene(0);
    }

    public void GoToMainMenu()
    {
        SetPause(false);

        SceneManager.LoadScene(1);
    }

    public void SetPause(bool isEnable)
    {
        Time.timeScale = isEnable ? 0 : 1;
    }

    void OnDestroy()
    {
        PauseWindow_UI.onPauseGame -= SetPause;
    }
}
