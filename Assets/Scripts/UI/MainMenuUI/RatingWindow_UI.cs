using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    private int _topPlacesCount = 3;


    void Start()
    {
        OptionsWindow_UI.onResetScore += ResetTopScore;

        if (MainMenu_UI._isGameStart)
        {
            for (int i = 1; i <= _topPlacesCount; i++)
            {
                ScoreCalculator.topScore.Add(PlayerPrefs.GetInt($"{i} place"));
            }
        }
        else
        {
            UpdateTopPlacesValues();
        }

        UpdateRatingUI();
    }   

    public void ShowRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(true);
    }

    public void HideRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(false);
    }

    public void ResetTopScore()
    {
        for (int i = 0; i < ScoreCalculator.topScore.Count; i++)
        {
            ScoreCalculator.topScore[i] = 0;
            UpdateTopPlacesValues();
            UpdateRatingUI();
        }
    }

    private void UpdateTopPlacesValues()
    {
        for (int i = 1, j = 0; i <= _topPlacesCount; i++, j++)
        {
            PlayerPrefs.SetInt($"{i} place", ScoreCalculator.topScore[j]);
        }
    }

    private void UpdateRatingUI()
    {
        for (int i = 0; i < _topPlaces.Length; i++)
        {
            _topPlaces[i].text = $"{ScoreCalculator.topScore[i]}";
        }
    }

    void OnDestroy()
    {
        OptionsWindow_UI.onResetScore -= ResetTopScore;
    }
}
