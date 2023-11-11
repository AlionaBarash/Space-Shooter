using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow_UI : MonoBehaviour
{
    [SerializeField]
    private Image _pauseWindow;

    public static Action<bool> onPauseGame;

    void Start()
    {
        GameManager.onPressingEscape += ShowPauseWindow;
    }

    private void ShowPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);

        onPauseGame?.Invoke(true);
    }

    public void ContinueGame()
    {
        _pauseWindow.gameObject.SetActive(false);

        onPauseGame?.Invoke(false);
    }

    void OnDestroy()
    {
        GameManager.onPressingEscape -= ShowPauseWindow;
    }

}
