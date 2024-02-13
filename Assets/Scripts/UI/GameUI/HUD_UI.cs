using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HUD_UI : MonoBehaviour
{
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private int[] _scoreLimits;
    [SerializeField]
    private RectTransform _buttonsPanel;
    [SerializeField]
    private Button _optionsButton;
    [SerializeField]
    private float _buttonPanelUpValue;

    public int _score { get; private set; } = 0;
    public static Action onWeightChange;

    private bool _isButtonsPanelActive;
    private List<int> _availableScoreLimits = new List<int>();



    void Start()
    {
        _availableScoreLimits.AddRange(_scoreLimits);

        Player.onPlayerDamage += UpdateLivesImage;

        Enemy.onEnemyDamage += UpdateScore;
        _scoreText.text = "SCORE: " + _score;

        GameManager.onGameProcessEnd += GetFinalScore;

        OnOptionsButtonClick();
    }

    void Update()
    {
        if (GameManager.instance.IsGameEnded)
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

        UpdateScoreLimits();
    }

    private void UpdateScoreLimits() 
    {
        for (int i = 0; i < _availableScoreLimits.Count; i++)
        {
            if (_score == _availableScoreLimits[i])
            {
                onWeightChange?.Invoke();
                _availableScoreLimits.Remove(_availableScoreLimits[i]);
            }
        }
    }

    private void OnOptionsButtonClick()
    {
        _optionsButton.onClick.AddListener(OptionsWindow_UI.instance.ShowOptionsWindow);
        _optionsButton.onClick.AddListener(() => GameManager.instance.SetPause(true));
    }

    public int GetFinalScore()
    {
        return _score;
    }

    public void HandleButtonsPanel()
    {
        if (!_isButtonsPanelActive)
        {
            _buttonsPanel.DOAnchorPos(Vector2.up * _buttonPanelUpValue, 0.5f);
            _isButtonsPanelActive = true;
        }
        else
        {
            _buttonsPanel.DOAnchorPos(Vector2.down, 0.5f);
            _isButtonsPanelActive = false;
        }
    }

    void OnDestroy()
    {
        Player.onPlayerDamage -= UpdateLivesImage;

        Enemy.onEnemyDamage -= UpdateScore;

        GameManager.onGameProcessEnd -= GetFinalScore;
    }

}
