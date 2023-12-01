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
        InitializeScoreSaveProcess();

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

    private void InitializeScoreSaveProcess()
    {
        for (int i = 1; i <= _topPlacesCount; i++)
        {
            if (!PlayerPrefs.HasKey($"{i} place"))
            {
                PlayerPrefs.SetInt($"{i} place", 0);
            }
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

    public void ShowRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(true);
    }

    public void HideRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(false);
    }
}
