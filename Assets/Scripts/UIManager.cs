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
    private TextMeshProUGUI _scoreText;

    private int _score = 0;

    void Start()
    {
        Player.onPlayerDamage += UpdateLivesImage;

        Enemy.onEnemyDamage += UpdateScore;

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

    void OnDestroy()
    {
        Player.onPlayerDamage -= UpdateLivesImage;

        Enemy.onEnemyDamage -= UpdateScore; 
    }
}
