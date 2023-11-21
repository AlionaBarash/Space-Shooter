using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatingWindow_UI : MonoBehaviour
{
    [SerializeField]
    private Image _ratingWindow;
    [SerializeField]
    private TextMeshProUGUI[] _topPlaces;

    void Start()
    {
        for (int i = 0; i < _topPlaces.Length; i++)
        {
            _topPlaces[i].text = $"{PlayerPrefs.GetInt($"{i + 1} place")}";
        } 
    }

    public void ShowRatingWindow()
    {
        _ratingWindow.gameObject.SetActive(true);
    }
}
