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

    void Start()
    {
        //юнити ивент или ивент?

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
        _pauseWindow.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        Player.onPlayerDamage -= UpdateLivesImage;
        Player.onPlayerDeath -= ShowGameOverWindow;

        Enemy.onEnemyDamage -= UpdateScore;

        GameManager.onPressingEscape -= ShowPauseWindow;
    }
}
