using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow_UI : MonoBehaviour
{
    [SerializeField]
    private Image _gameOverWindow;

    void Start()
    {
        Player.onPlayerDeath += ShowGameOverWindow;
    }

    private void ShowGameOverWindow()
    {
        _gameOverWindow.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        Player.onPlayerDeath -= ShowGameOverWindow;
    }


}
