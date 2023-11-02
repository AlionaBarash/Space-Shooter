using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _gameOverWindow;
    [SerializeField]
    private Image _pauseWindow;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int _score = 0;

    public static Action<bool> onGamePause;

    void Start()
    {
        Player.onPlayerDamage += UpdateLivesImage;
        Player.onPlayerDeath += ShowGameOverWindow;

        Enemy.onEnemyDamage += UpdateScore;

        GameManager.onPressingEscape += ShowPauseWindow;

        _scoreText.text = "Score: " + _score;
    }

    private void UpdateLivesImage(int playerHealth)
    {
        _livesImage.sprite = _livesSprites[playerHealth];
    }

    private void UpdateScore()
    {
        _score += 10;

        _scoreText.text = "Score: " + _score;
    }

    private void ShowGameOverWindow()
    {
       _gameOverWindow.gameObject.SetActive(true);
    }

    private void ShowPauseWindow()
    {
        onGamePause?.Invoke(true);

        _pauseWindow.gameObject.SetActive(true);
    }

    public void ContinueGame()
    {
        onGamePause?.Invoke(false);

        _pauseWindow.gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        Player.onPlayerDamage -= UpdateLivesImage;
        Player.onPlayerDeath -= ShowGameOverWindow;

        Enemy.onEnemyDamage -= UpdateScore;

        GameManager.onPressingEscape -= ShowPauseWindow;
    }
}
