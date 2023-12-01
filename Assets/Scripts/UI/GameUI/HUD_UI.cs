using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_UI : MonoBehaviour
{
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Button _optionsButton;

    public int _score { get; private set; } = 0;

    void Start()
    {
        Player.onPlayerDamage += UpdateLivesImage;

        Enemy.onEnemyDamage += UpdateScore;
        _scoreText.text = "SCORE: " + _score;

        GameManager.onGameProcessEnd += GetFinalScore;

        _optionsButton.onClick.AddListener(OptionsWindow_UI.instance.ShowOptionsWindow);
    }

    void Update()
    {
        if (GameManager.instance._isGameEnded)
        {
            var _scoreCalculator = new ScoreCalculator();
            _scoreCalculator.AddToTopScore(_score);
        }
    }

    private void UpdateLivesImage(int playerHealth)
    {
        _livesImage.sprite = _livesSprites[playerHealth];
    }

    private void UpdateScore()
    {
        _score += 10;

        _scoreText.text = "SCORE: " + _score;
    }

    public int GetFinalScore()
    {
        return _score;
    }

    void OnDestroy()
    {
        Player.onPlayerDamage -= UpdateLivesImage;

        Enemy.onEnemyDamage -= UpdateScore;

        GameManager.onGameProcessEnd -= GetFinalScore;
    }

}
