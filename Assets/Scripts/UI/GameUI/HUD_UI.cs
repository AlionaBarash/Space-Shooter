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

    private int _score = 0;

    void Start()
    {
        Player.onPlayerDamage += UpdateLivesImage;

        Enemy.onEnemyDamage += UpdateScore;
        _scoreText.text = "SCORE: " + _score;
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

    void OnDestroy()
    {
        Player.onPlayerDamage -= UpdateLivesImage;

        Enemy.onEnemyDamage -= UpdateScore;
    }

}
