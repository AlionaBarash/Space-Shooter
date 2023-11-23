using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RatingWindow_UI : MonoBehaviour
{
    [SerializeField]
    private Image _ratingWindow;
    [SerializeField]
    private TextMeshProUGUI[] _topPlaces;
    [SerializeField]
    private RatingNumber[] _savedTopPlacesValues;

    static private bool _isGameStart = true;

    void Start()
    {
        if (_isGameStart)
        {
            for (int i = 0; i < _savedTopPlacesValues.Length; i++)
            {
                ScoreCalculator.topScore.Add(_savedTopPlacesValues[i].ratingScoreValue);
            }

            _isGameStart = false;
        }
        else
        {
            UpdateTopPlacesValues();
        }

        UpdateRatingUI();
    }

    private void UpdateTopPlacesValues()
    {
        for (int i = 0; i < _savedTopPlacesValues.Length; i++)
        {
            _savedTopPlacesValues[i].ratingScoreValue = ScoreCalculator.topScore[i];
        }
    }

    private void UpdateRatingUI()
    {
        for (int i = 0; i < _topPlaces.Length; i++)
        {
            _topPlaces[i].text = $"{_savedTopPlacesValues[i].ratingScoreValue}";
        }
    }

    public void ShowRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(true);
    }

    public void HideRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(false);
    }
}
