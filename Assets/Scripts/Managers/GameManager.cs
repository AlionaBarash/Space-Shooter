using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private TopScore _topScore = new TopScore();

    public static GameManager instance;

    public static Func<int> onGameProcessEnd;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void RestartGame() // before - save score
    {
        SetPause(false);

        if (onGameProcessEnd != null)
            _topScore.AddToTopScore(onGameProcessEnd.Invoke());

        Debug.Log("First place: " + _topScore.topScore[0]);
        Debug.Log("Second place: " + _topScore.topScore[1]);
        Debug.Log("Third place: " + _topScore.topScore[2]);

        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu() // before - save score
    {
        SetPause(false);

        if (onGameProcessEnd != null)
            _topScore.AddToTopScore(onGameProcessEnd.Invoke());

        Debug.Log("First place: " + _topScore.topScore[0]);
        Debug.Log("Second place: " + _topScore.topScore[1]);
        Debug.Log("Third place: " + _topScore.topScore[2]);

        SceneManager.LoadScene(0);
    }

    public void SetPause(bool isEnable)
    {
        Time.timeScale = isEnable ? 0 : 1;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
