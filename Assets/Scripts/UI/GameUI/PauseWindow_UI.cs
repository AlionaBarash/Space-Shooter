using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow_UI : MonoBehaviour
{
    [SerializeField]
    private Image _pauseWindow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseWindow();
        }
    }

    public void ShowPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);

        GameManager.instance.SetPause(true);
    }

    public void ContinueGame()
    {
        _pauseWindow.gameObject.SetActive(false);

        GameManager.instance.SetPause(false);
    }
}
